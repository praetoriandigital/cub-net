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
            }
            return null;
        }
    }
}
