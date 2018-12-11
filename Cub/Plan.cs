namespace Cub
{
    public class Plan : CObject
    {
        public Plan()
        {
        }

        public Plan(Plan obj)
            : base(obj)
        {
        }

        public static Plan Get(string id)
        {
            return BaseGet<Plan>(id, null);
        }

        private string ProductUid => _string("product");

        public Product Product => string.IsNullOrEmpty(ProductUid) ? null : Product.Get(ProductUid);
    }
}
