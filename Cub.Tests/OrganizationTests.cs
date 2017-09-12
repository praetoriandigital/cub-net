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
              ""country"": {
                ""id"": ""cry_3zxJkF8kKdowkmRp"",
                ""name"": ""United States"",
                ""object"": ""country""
              },
              ""employees"": ""11-50"",
              ""id"": ""org_tBLeinLfH4yG4fJe"",
              ""name"": ""Abbeville Police Department"",
              ""object"": ""organization"",
              ""state"": {
                ""id"": ""stt_8mLCuP25Sbs1dHzJ"",
                ""name"": ""Louisiana"",
                ""object"": ""state""
              }
            }";

            var organization = CObjectFactory.FromJObject(JObject.Parse(json)) as Organization;

            Assert.NotNull(organization);
            Assert.IsNotNullOrEmpty(organization.Id);
            Assert.AreEqual(organization.CountryName, "United States");
            Assert.AreEqual(organization.StateName, "Louisiana");
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

            var organization = lead.Data.GetOrganization();

            Assert.NotNull(organization);
            Assert.AreEqual(organization.Name, "Abbeville Police Department");
        }
    }
}
