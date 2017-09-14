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
    }
}
