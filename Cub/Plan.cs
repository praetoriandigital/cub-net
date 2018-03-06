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

        public string Name => _string("name");
    }
}
