using System.Collections.Generic;

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

        public static Organization Get(string id)
        {
            return BaseGet<Organization>(id, null);
        }

        public string City => _string("city");

        public string Employees => _string("employees");

        public string Name => _string("name");

        private string CountryUid => _string("country");

        private string StateUid => _string("state");

        public Country Country => Country.Get(CountryUid);

        public State State => State.Get(StateUid);

        public string Address => _string("address");

        public string PostalCode => _string("postal_code");

        public string Phone => _string("phone");

        public string Website => _string("website");

        public string Logo => _string("logo");

        public ICollection<string> Tags => _list<string>("tags");
    }
}
