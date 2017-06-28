using Newtonsoft.Json.Linq;

using NUnit.Framework;

namespace Cub.Tests
{
    public class LeadTests
    {
        [Test]
        public void CreateMailingListFromJToken()
        {
            var json = @"{
                      ""object"": ""lead"",
                      ""id"": ""led_i9uMBoLkgIY6BrUR"",
                      ""data"": {
                        ""first_name"": ""Oleg"",
                        ""last_name"": ""Shevchenko"",
                        ""organization_name"": ""1"",
                        ""organization_size"": ""1-10"",
                        ""zip"": ""163000"",
                        ""phone"": ""9539399200"",
                        ""purchase_for_organization"": ""No"",
                        ""purchasing_timeframe"": ""Research only at this time"",
                        ""company"": ""ATS Armor"",
                        ""notifications"": ""p1:campaign:54264001"",
                        ""source"": ""Article Lead Block""
                      },
                      ""field_labels"": {},
                      ""user"": null,
                      ""email"": ""olsh.me@gmail.com"",
                      ""site"": ""ste_5KCatbhRv6rGMXTk"",
                      ""url"": ""http://policeone.local/police-products/tactical/ballistic-shields/articles/151262006-QuadCurve-How-ATS-Armor-provides-the-most-comfortable-protection-available/"",
                      ""remote_ip"": ""52.174.51.233"",
                      ""created"": ""2017-06-28T10:31:29Z"",
                      ""deleted"": true
                    }";

            var lead = CObjectFactory.FromJObject(JObject.Parse(json)) as Lead;

            Assert.NotNull(lead);
            Assert.NotNull(lead.Data);
            Assert.IsNotNullOrEmpty(lead.Id);
            Assert.IsNotNullOrEmpty(lead.Data.FirstName);
            Assert.IsTrue(lead.Deleted == true);
        }
    }
}
