namespace Cub
{
    public class Customer : CObject
    {
        public Customer()
        {
        }

        public Customer(Customer obj)
            : base(obj)
        {
        }

        public static Customer Get(string id)
        {
            return BaseGet<Customer>(id, null);
        }

        private string OrganizationUid => _string("organization");

        private string UserUid => _string("user");

        public Organization Organization => string.IsNullOrEmpty(OrganizationUid) ? null : Organization.Get(OrganizationUid);

        public User User => string.IsNullOrEmpty(UserUid) ? null : User.GetByUid(UserUid);
    }
}
