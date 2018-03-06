using Newtonsoft.Json.Linq;

using NUnit.Framework;

namespace Cub.Tests
{
    public class ServiceSubscriptionTests
    {
        [Test]
        public void CreateServiceSubscriptionFromJToken()
        {
            var json = @"{
              ""object"": ""servicesubscription"",
              ""id"": ""ssu_2rYsoMxJGbzHzt9F"",
              ""customer"": ""cus_i29BBvyMq1H1tPFo"",
              ""plan"": ""pln_fFkXL6xWdSIWuZtE"",
              ""active_since"": ""2018-03-05T15:21:26Z"",
              ""active_till"": ""2018-03-30T15:21:28Z"",
            }";

            var serviceSubscription = CObjectFactory.FromJObject(JObject.Parse(json)) as ServiceSubscription;

            Assert.NotNull(serviceSubscription);
        }
    }
}
