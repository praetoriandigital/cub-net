using System.Collections.Generic;

namespace Cub
{
    public class Organization : ExpandableCObject<Organization>
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

        public Country Country => _expandable<Country>("country");

        public State State => _expandable<State>("state");

        public string Address => _string("address");

        public string PostalCode => _string("postal_code");

        public string Phone => _string("phone");

        public string Website => _string("website");

        public string Logo => _string("logo");

        public ICollection<string> Tags => _list<string>("tags");

        public string Email => _string("email");

        public void UploadLogo(string url)
        {
            var parameters = new Dictionary<string, object> {["url"] = url};
            Api.RequestObject("POST", $"organizations/{Id}/logo", parameters, ApiKey);
            BaseReload();
        }

        public void DeleteLogo()
        {
            Api.DeleteImage($"organizations/{Id}/logo", ApiKey);
            BaseReload();
        }
    }
}
