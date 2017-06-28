using Newtonsoft.Json;

namespace Cub
{
    public class LeadData
    {
        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("notifications")]
        public string Notifications { get; set; }

        [JsonProperty("organization_name")]
        public string OrganizationName { get; set; }

        [JsonProperty("organization_size")]
        public string OrganizationSize { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("purchase_for_organization")]
        public string PurchaseForOrganization { get; set; }

        [JsonProperty("purchasing_timeframe")]
        public string PurchasingTimeFrame { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }
    }
}
