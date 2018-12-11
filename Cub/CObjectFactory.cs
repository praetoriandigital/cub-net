using Newtonsoft.Json.Linq;

namespace Cub
{
    public class CObjectFactory
    {
        public static CObject FromJObject(JObject obj)
        {
            if (obj?["object"] == null || obj["id"] == null)
                return null;
            var objType = obj["object"].Value<string>();
            switch (objType)
            {
                case "user":
                    return new User().FromObject(obj);
                case "subscription":
                    return new Subscription().FromObject(obj);
                case "mailinglist":
                    return new MailingList().FromObject(obj);
                case "lead":
                    return new Lead().FromObject(obj);
                case "organization":
                    return new Organization().FromObject(obj);
                case "plan":
                    return new Plan().FromObject(obj);
                case "servicesubscription":
                    return new ServiceSubscription().FromObject(obj);
            }

            return null;
        }
    }
}
