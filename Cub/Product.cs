namespace Cub
{
    public class Product : CObject
    {
        public Product()
        {
        }

        public Product(Plan obj)
            : base(obj)
        {
        }

        public static Product Get(string id)
        {
            return BaseGet<Product>(id, null);
        }

        public string Name => _string("name");

        public string Type => _string("type");
    }
}
