using System.Linq;

using Newtonsoft.Json.Linq;

using NUnit.Framework;

namespace Cub.Tests
{
    public class LeadTests
    {
        [Test]
        public void CreateLeadFromJToken()
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
                        ""campaign_id"": ""54264001"",
                        ""source"": ""Article Lead Block""
                      },
                      ""field_labels"": {},
                      ""user"": null,
                      ""email"": ""olsh.me@gmail.com"",
                      ""site"": ""ste_5KCatbhRv6rGMXTk"",
                      ""url"": ""http://policeone.local/police-products/tactical/ballistic-shields/articles/151262006-QuadCurve-How-ATS-Armor-provides-the-most-comfortable-protection-available/"",
                      ""remote_ip"": ""52.174.51.233"",
                      ""created"": ""2017-06-28T10:31:29Z"",
                      ""production"": true,
                      ""deleted"": true
                    }";

            var lead = CObjectFactory.FromJObject(JObject.Parse(json)) as Lead;

            Assert.NotNull(lead);
            Assert.IsTrue(lead.Created.Year == 2017);
            Assert.NotNull(lead.Data);
            Assert.IsNotNullOrEmpty(lead.Id);
            Assert.IsNotNullOrEmpty(lead.Data.FirstName);
            Assert.IsTrue(lead.Deleted == true);
            Assert.IsTrue(lead.IsProduction);
            var products = lead.Data.GetProducts();
            Assert.True(!products.Any());
        }

        [Test]
        public void CreateLeadWithArrayOfProductsFromJToken()
        {
            var json = @"{
                  ""object"": ""lead"",
                  ""id"": ""led_5xRVegpeb79bdrgK"",
                  ""data"": {
                    ""company"": ""PoliceOne Academy"",
                    ""first_name"": ""Oleg"",
                    ""last_name"": ""Shevchenko"",
                    ""position"": ""Assistant Chief"",
                    ""organization_name"": ""312"",
                    ""organization_city"": ""Onega"",
                    ""state"": ""Alabama"",
                    ""zip"": ""164840"",
                    ""phone"": ""9539399200"",
                    ""organization_area_type"": ""Urban"",
                    ""interested_in_products"": ""No"",
                    ""purchase_for_organization"": ""Not Sure"",
                    ""utilized"": ""Not Sure"",
                    ""comments"": ""test comment"",
                    ""products"": [ ""first"", ""second"" ]
                  },
                  ""field_labels"": {},
                  ""user"": null,
                  ""email"": ""olsh.me@gmail.com"",
                  ""site"": ""ste_PRiWz9RP8gfy9QnP"",
                  ""url"": ""http://policegrantshelp.local/PoliceOne-Academy-Grant-Assistance/"",
                  ""remote_ip"": ""52.174.51.233"",
                  ""created"": ""2017-06-29T11:28:50Z""
                }";

            var lead = CObjectFactory.FromJObject(JObject.Parse(json)) as Lead;

            Assert.NotNull(lead);
            Assert.NotNull(lead.Data);
            Assert.IsNotNullOrEmpty(lead.Id);
            Assert.IsNotNullOrEmpty(lead.Data.FirstName);
            var products = lead.Data.GetProducts();
            Assert.IsTrue(products.Count() == 2);
        }

        [Test]
        public void CreateLeadWithObjectOfProductsFromJToken()
        {
            var json = @"{
                  ""object"": ""lead"",
                  ""id"": ""led_5xRVegpeb79bdrgK"",
                  ""data"": {
                    ""company"": ""PoliceOne Academy"",
                    ""first_name"": ""Oleg"",
                    ""last_name"": ""Shevchenko"",
                    ""position"": ""Assistant Chief"",
                    ""organization_name"": ""312"",
                    ""organization_city"": ""Onega"",
                    ""state"": ""Alabama"",
                    ""zip"": ""164840"",
                    ""phone"": ""9539399200"",
                    ""organization_area_type"": ""Urban"",
                    ""interested_in_products"": ""No"",
                    ""purchase_for_organization"": ""Not Sure"",
                    ""utilized"": ""Not Sure"",
                    ""comments"": ""test comment"",
                    ""products"": {
    	                ""first"": ""first_value"",
    	                ""second"": ""second_value""
                    }
                  },
                  ""field_labels"": {},
                  ""user"": null,
                  ""email"": ""olsh.me@gmail.com"",
                  ""site"": ""ste_PRiWz9RP8gfy9QnP"",
                  ""url"": ""http://policegrantshelp.local/PoliceOne-Academy-Grant-Assistance/"",
                  ""remote_ip"": ""52.174.51.233"",
                  ""created"": ""2017-06-29T11:28:50Z""
                }";

            var lead = CObjectFactory.FromJObject(JObject.Parse(json)) as Lead;

            Assert.NotNull(lead);
            Assert.NotNull(lead.Data);
            Assert.IsNotNullOrEmpty(lead.Id);
            Assert.IsNotNullOrEmpty(lead.Data.FirstName);
            var products = lead.Data.GetProducts();
            Assert.IsTrue(products.Count() == 2);
        }

        [Test]
        public void CreateLeadWithStringOfProductsFromJToken()
        {
            var json = @"{
                  ""object"": ""lead"",
                  ""id"": ""led_5xRVegpeb79bdrgK"",
                  ""data"": {
                    ""company"": ""PoliceOne Academy"",
                    ""first_name"": ""Oleg"",
                    ""last_name"": ""Shevchenko"",
                    ""position"": ""Assistant Chief"",
                    ""organization_name"": ""312"",
                    ""organization_city"": ""Onega"",
                    ""state"": ""Alabama"",
                    ""zip"": ""164840"",
                    ""phone"": ""9539399200"",
                    ""organization_area_type"": ""Urban"",
                    ""interested_in_products"": ""No"",
                    ""purchase_for_organization"": ""Not Sure"",
                    ""utilized"": ""Not Sure"",
                    ""comments"": ""test comment"",
                    ""products"": ""single_product""
                  },
                  ""field_labels"": {},
                  ""user"": null,
                  ""email"": ""olsh.me@gmail.com"",
                  ""site"": ""ste_PRiWz9RP8gfy9QnP"",
                  ""url"": ""http://policegrantshelp.local/PoliceOne-Academy-Grant-Assistance/"",
                  ""remote_ip"": ""52.174.51.233"",
                  ""created"": ""2017-06-29T11:28:50Z""
                }";

            var lead = CObjectFactory.FromJObject(JObject.Parse(json)) as Lead;

            Assert.NotNull(lead);
            Assert.NotNull(lead.Data);
            Assert.IsNotNullOrEmpty(lead.Id);
            Assert.IsNotNullOrEmpty(lead.Data.FirstName);
            var products = lead.Data.GetProducts();
            Assert.IsTrue(products.Count() == 1);
        }
    }
}
