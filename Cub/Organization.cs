using Newtonsoft.Json;

namespace Cub
{
    public class Organization : CObject
    {
        public Organization()
        {
        }

        public Organization(Organization obj)
            : base(obj)
        {
        }

        public string City => _string("city");

        public string Employees => _string("employees");

        public string Name => _string("name");

        public string CountryName => _refType<Country>("country")?.Name;

        public string StateName => _refType<State>("state")?.Name;

        internal class Country
        {
            [JsonProperty("name")]
            public string Name { get; set; }
        }

        internal class State
        {
            [JsonProperty("name")]
            public string Name { get; set; }
        }
    }
}
