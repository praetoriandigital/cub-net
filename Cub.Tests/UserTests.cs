using NUnit.Framework;

namespace Cub.Tests
{
    class AccountTests
    {
        [Test]
        public void UserLoginAndGetByToken()
        {
            var user = User.Login("den", "den");
            Assert.AreEqual("slow", user.FirstName);
            Assert.AreEqual("poke", user.LastName);
            Assert.NotNull(user.P1MemberId);
            Assert.NotNull(user.Token);
            Assert.True(user.Token.StartsWith("t_"));

            var user2 = User.Get(user.Token);
            Assert.AreEqual(user.FirstName, user2.FirstName);
            Assert.AreEqual(user.LastName, user2.LastName);
            Assert.AreEqual(user.Email, user2.Email);
            Assert.AreEqual(user.Username, user2.Username);
            Assert.AreEqual(user.Token, user2.Token);
        }
    }
}
