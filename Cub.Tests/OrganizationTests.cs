using System;
using System.IO;
using Newtonsoft.Json.Linq;

using NUnit.Framework;

namespace Cub.Tests
{
    public class OrganizationTests
    {
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
            Assert.AreEqual(organization.Country?.Name, "United States");
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

            var path = Path.Combine(Path.GetTempPath(), $"{organization.Id}_logo.png");
            const string content = "iVBORw0KGgoAAAANSUhEUgAAAQAAAAEAAQMAAABmvDolAAAAA1BMVEW10NBjBBbqAAAAH0lEQVRoge3BAQ0AAADCoPdPbQ43oAAAAAAAAAAAvg0hAAABmmDh1QAAAABJRU5ErkJggg==";
            var bytes = Convert.FromBase64String(content);
            File.WriteAllBytes(path, bytes);

            Assert.Throws<ForbiddenException>(() => organization.DeleteLogo());
            Assert.Throws<ForbiddenException>(() => organization.UploadLogo(path));

            File.Delete(path);
        }
    }
}
