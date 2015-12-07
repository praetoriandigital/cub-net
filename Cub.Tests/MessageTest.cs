using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Cub.Tests
{
    class MessageTest
    {
        [Test]
        public void MessageSearch()
        {
            // Find messages by FC database
            var messages = Message.List(new Dictionary<string, object>
            {
                { "FCDB", "p1" }
            });
            Assert.Greater(messages.Count, 0);
            var message = messages[0];
            Assert.GreaterOrEqual(message.TotalBounces, 0);
            Assert.GreaterOrEqual(message.TotalDelivered, 0);
            Assert.GreaterOrEqual(message.TotalOpens, 0);
            Assert.GreaterOrEqual(message.UniqueOpens, 0);

            Assert.GreaterOrEqual(message.TotalDelivered, message.TotalOpens);
            Assert.GreaterOrEqual(message.TotalOpens, message.UniqueOpens);

            Assert.NotNull(message.Subject);
            Assert.LessOrEqual(message.Created, DateTime.UtcNow);
            Assert.AreEqual(message.FCDB, "p1");
            Assert.Greater(message.FCNewsletterID, 0);

            // Get particular message by ID
            var message2 = Message.Get(message.Id);
            Assert.AreEqual(message.Subject, message2.Subject);
            Assert.AreEqual(message.FCDB, message2.FCDB);
            Assert.AreEqual(message.FCNewsletterID, message2.FCNewsletterID);

            Assert.AreEqual(message.TotalBounces, message2.TotalBounces);
            Assert.AreEqual(message.TotalDelivered, message2.TotalDelivered);
            Assert.AreEqual(message.TotalOpens, message2.TotalOpens);
            Assert.AreEqual(message.UniqueOpens, message2.UniqueOpens);

            Assert.AreEqual(message.Created, message2.Created);
        }
    }
}
