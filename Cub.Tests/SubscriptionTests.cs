using Newtonsoft.Json.Linq;

using NUnit.Framework;

namespace Cub.Tests
{
    public class SubscriptionTests
    {
        [Test]
        public void CreateSubscriptionFromJToken()
        {
            var json = @"{
              ""object"": ""subscription"",
              ""id"": ""sub_LZmtXuQcuWeHDHFm"",
              ""mailinglist"": ""mlt_u8f14e45fceea167"",
              ""user"": ""usr_w250IuJqQcLsOy8y"",
              ""invitation"": null
            }";

            var subscription = CObjectFactory.FromJObject(JObject.Parse(json)) as Subscription;


            Assert.NotNull(subscription);
            Assert.IsNotNullOrEmpty(subscription.MailingList);
            Assert.IsNotNullOrEmpty(subscription.User);
        }
    }
}
