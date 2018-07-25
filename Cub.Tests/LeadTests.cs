using System;
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

        [Test]
        public void LoadLeadWithOrganizationField()
        {
            var json = @"{
                ""object"": ""lead"", 
                ""id"": ""led_Ko3tYub3lqdZfHd8"",
            }";
            var lead = CObjectFactory.FromJObject(JObject.Parse(json)) as Lead;
            Assert.NotNull(lead);

            lead.Reload(new[] {"organization__country", "organization__state"});
            Assert.NotNull(lead);

            Assert.NotNull(lead.Organization);
            Assert.NotNull(lead.Organization.Country);
            Assert.NotNull(lead.Organization.State);
        }

        [Test]
        public void LoadLeadsWithFilter()
        {
            var from = new DateTime(2017, 9, 14);
            var to = new DateTime(2017, 9, 15);
            var leads = Lead.List(from: from, to: to);
            foreach (var lead in leads)
            {
                Assert.GreaterOrEqual(lead.Created, from);
                Assert.LessOrEqual(lead.Created, to);
            }

            leads = Lead.List(offset: 0, count: 1);
            Assert.AreEqual(leads.Count, 1);
        }

        [Test]
        public void LoadLeadWithExpands()
        {
            var json = @"{
                ""object"": ""lead"", 
                ""id"": ""led_Ko3tYub3lqdZfHd8"",
            }";
            var lead = CObjectFactory.FromJObject(JObject.Parse(json)) as Lead;
            Assert.NotNull(lead);

            // reload without expands
            lead.Reload();
            Assert.Null(lead.Organization.City);
            Assert.Null(lead.Organization.Name);
            Assert.Null(lead.Organization.Country);

            // reload only with organization
            lead.Reload(new[] {"organization"});
            Assert.NotNull(lead.Organization.City);
            Assert.NotNull(lead.Organization.Name);
            Assert.Null(lead.Organization.Country.Name);
            Assert.Null(lead.Organization.State.Name);

            // reload with country and state
            lead.Reload(new[] {"organization__country", "organization__state"});
            Assert.NotNull(lead.Organization.Country.Name);
            Assert.NotNull(lead.Organization.State.Name);

            // reload without expands again
            lead.Reload();
            Assert.Null(lead.Organization.City);
            Assert.Null(lead.Organization.Name);
            Assert.Null(lead.Organization.Country);
        }

        [Test]
        public void LoadLeadsWithExpands()
        {
            var from = new DateTime(2018, 7, 24);
            var leads = Lead.List(0, 3, from: from);
            foreach (var lead in leads)
            {
                Assert.NotNull(lead.Organization);
                Assert.Null(lead.Organization.Name);
            }

            leads = Lead.List(0, 3, from: from, expands: new[] {"organization__country", "organization__state"});
            foreach (var lead in leads)
            {
                Assert.NotNull(lead.Organization.Country.Name);
                Assert.NotNull(lead.Organization.State.Name);
            }
        }
    }
}
