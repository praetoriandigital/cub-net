using Newtonsoft.Json.Linq;

namespace Cub
{
    public class CObjectFactory
    {
        public static CObject FromJObject(JObject obj)
        {
            if (obj == null)
                return null;
            if (obj["object"] == null || obj["id"] == null)
                return null;
            var objType = obj["object"].Value<string>();
            switch (objType)
            {
                case "user":
                    var user = new User();
                    user.FromObject(obj);
                    return user;
                case "subscription":
                    var subscription = new Subscription();
                    subscription.FromObject(obj);
                    return subscription;
                case "mailinglist":
                    var mailingList = new MailingList();
                    mailingList.FromObject(obj);
                    return mailingList;
            }

            return null;
        }
    }
}
