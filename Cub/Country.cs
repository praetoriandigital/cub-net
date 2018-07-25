namespace Cub
{
    public class Country : CObject
    {
        public Country()
        {
        }

        public Country(Country obj)
            : base(obj)
        {
        }

        public static Country Get(string id)
        {
            return BaseGet<Country>(id, null);
        }

        public override string InstanceUrl => $"country/{Id}";

        public string Name => _string("name");

        public Country Reload()
        {
            BaseReload();
            return this;
        }
    }
}
