using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cub
{
    public class LeadData
    {
        [JsonProperty("category_id")]
        public decimal CategoryId { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("comments")]
        public string Comments { get; set; }

        [JsonProperty("community_size")]
        public string CommunitySize { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("custom_1")]
        public string Custom1 { get; set; }

        [JsonProperty("custom_2")]
        public string Custom2 { get; set; }

        [JsonProperty("custom_3")]
        public string Custom3 { get; set; }

        [JsonProperty("custom_4")]
        public string Custom4 { get; set; }

        [JsonProperty("editor_notes")]
        public string EditorNotes { get; set; }

        [JsonProperty("estimated_amount")]
        public string EstimatedAmount { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("funded_amount")]
        public decimal FundedAmount { get; set; }

        [JsonProperty("grant_selected")]
        public string GrantSelected { get; set; }

        [JsonProperty("interested_in_products")]
        public string InterestedInProducts { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("looking_for_other")]
        public string LookingForOther { get; set; }

        [JsonProperty("member_position")]
        public string MemberPosition { get; set; }

        [JsonProperty("notifications")]
        public string Notifications { get; set; }

        [JsonProperty("organization_address")]
        public string OrganizationAddress { get; set; }

        [JsonProperty("organization_area_type")]
        public string OrganizationAreaType { get; set; }

        [JsonProperty("organization_city")]
        public string OrganizationCity { get; set; }

        [JsonProperty("organization_name")]
        public string OrganizationName { get; set; }

        [JsonProperty("organization_size")]
        public string OrganizationSize { get; set; }

        [JsonProperty("organization_type")]
        public string OrganizationType { get; set; }

        [JsonProperty("organization_type_other")]
        public string OrganizationTypeOther { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("position_other")]
        public string PositionOther { get; set; }

        [JsonProperty("purchase_for_organization")]
        public string PurchaseForOrganization { get; set; }

        [JsonProperty("purchasing_timeframe")]
        public string PurchasingTimeFrame { get; set; }

        [JsonProperty("qualified")]
        public string Qualified { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("tax_status")]
        public string TaxStatus { get; set; }

        [JsonProperty("utilized")]
        public string Utilized { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("workflow")]
        public string Workflow { get; set; }

        [JsonProperty("grant_assistance")]
        internal JToken GrantAssistance { get; set; }

        [JsonProperty("looking_for")]
        internal JToken LookingFor { get; set; }

        [JsonProperty("products")]
        internal JToken Products { get; set; }

        public IEnumerable<string> GetGrantAssistance()
        {
            return GetNormalizedCollection(GrantAssistance);
        }

        public IEnumerable<string> GetLookingFor()
        {
            return GetNormalizedCollection(LookingFor);
        }

        public IEnumerable<string> GetProducts()
        {
            return GetNormalizedCollection(Products);
        }

        private IEnumerable<string> GetNormalizedCollection(JToken property)
        {
            if (property == null)
            {
                return new string[0];
            }

            if (property.Type == JTokenType.Array)
            {
                return property.Values<string>();
            }

            if (property.Type == JTokenType.Object)
            {
                var products = new LinkedList<string>();
                foreach (var keyValuePair in (JObject)property)
                {
                    products.AddLast($"{keyValuePair.Key}: {keyValuePair.Value}");
                }

                return products;
            }

            if (property.Type == JTokenType.String)
            {
                return new[]
                           {
                               property.Value<string>()
                                   ?.Trim()
                           };
            }

            return new string[0];
        }
    }
}
