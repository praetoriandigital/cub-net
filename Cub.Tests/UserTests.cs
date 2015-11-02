using NUnit.Framework;

namespace Cub.Tests
{
    class AccountTests
    {
        [Test]
        public void UserLoginAndGetByToken()
        {
            var user = User.Login("support@ivelum.com", "SJW8Gg");
            Assert.AreEqual("do not remove of modify", user.FirstName);
            Assert.AreEqual("user for tests", user.LastName);
            Assert.NotNull(user.Token);
            Assert.True(user.Token.StartsWith("tok_"));
            Assert.False(user.EmailConfirmed);

            var user2 = User.Get(user.Token);
            Assert.AreEqual(user.FirstName, user2.FirstName);
            Assert.AreEqual(user.LastName, user2.LastName);
            Assert.AreEqual(user.Email, user2.Email);
            Assert.AreEqual(user.Username, user2.Username);
            Assert.AreEqual(user.Token, user2.Token);
        }
    }
}
