using Newtonsoft.Json.Linq;

using NUnit.Framework;

namespace Cub.Tests
{
    public class MailingListTests
    {
        [Test]
        public void CreateMailingListFromJToken()
        {
            var json = @"{
                  ""object"": ""mailinglist"",
                  ""id"": ""mlt_CQetWcANkTl6Ag0A"",
                  ""deleted"": true
                }";

            var mailingList = CObjectFactory.FromJObject(JObject.Parse(json)) as MailingList;

            Assert.NotNull(mailingList);
            Assert.IsNotNullOrEmpty(mailingList.Id);
            Assert.IsTrue(mailingList.Deleted == true);
        }
    }
}
