namespace Cub
{
    public class Subscription : CObject
    {
        public Subscription()
        {
        }

        public Subscription(Subscription obj)
            : base(obj)
        {
        }

        public string MailingList => _string("mailinglist");

        public string User => _string("user");
    }
}
