using Newtonsoft.Json.Linq;

using NUnit.Framework;
using System.Net;

namespace Cub.Tests
{
    public class OrganizationTests
    {
        [SetUp]
        public void SetUp()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        [Test]
        public void CreateOrganizationFromJToken()
        {
            var json = @"{
              ""city"": ""Abbeville"",
              ""country"": ""cry_3zxJkF8kKdowkmRp"",
              ""employees"": ""11-50"",
              ""id"": ""org_tBLeinLfH4yG4fJe"",
              ""name"": ""Abbeville Police Department"",
              ""object"": ""organization"",
              ""state"": ""stt_8mLCuP25Sbs1dHzJ""
            }";

            var organization = CObjectFactory.FromJObject(JObject.Parse(json)) as Organization;

            Assert.NotNull(organization);
            Assert.IsNotNullOrEmpty(organization.Id);

            organization.Country.Reload();
            Assert.AreEqual(organization.Country?.Name, "United States");

            organization.State.Reload();
            Assert.AreEqual(organization.State?.Name, "Louisiana");
        }

        [Test]
        public void CreateOrganizationFromLead()
        {
            var json = @"{
              ""object"": ""lead"",
              ""id"": ""led_i9uMBoLkgIY6BrUR"",
              ""data"": {
                ""organization"": ""org_tBLeinLfH4yG4fJe""
              }
            }";

            var lead = CObjectFactory.FromJObject(JObject.Parse(json)) as Lead;

            Assert.NotNull(lead);
            Assert.IsNotNullOrEmpty(lead.Id);

            var organization = lead.Data.Organization;

            Assert.NotNull(organization);
            Assert.AreEqual(organization.Name, "Abbeville Police Department");
            Assert.AreEqual(organization.Address, "304 Charity St");
            Assert.AreEqual(organization.PostalCode, "70510-5131");
        }

        [Test]
        public void Get_RealOrgWithTags_ShouldReturnTags()
        {
            var organization = Organization.Get("org_tBLeinLfH4yG4fJe");

            Assert.NotNull(organization);
            Assert.AreEqual(1, organization.Tags.Count);
            Assert.True(organization.Tags.Contains("Law Enforcement"));
        }

        [Test]
        public void UploadLogo()
        {
            var organization = Organization.Get("org_tBLeinLfH4yG4fJe");

            var imageUrl = "https://raw.githubusercontent.com/praetoriandigital/cub-docs/master/cub-logo.png";
            Assert.Throws<ForbiddenException>(() => organization.DeleteLogo());
            Assert.Throws<ForbiddenException>(() => organization.UploadLogo(imageUrl));
        }
    }
}
