using System;
using System.IO;
using NUnit.Framework;

namespace Cub.Tests
{
    public class UserTests
    {
        [Test]
        public void UserLoginAndGetByToken()
        {
            var user = Login();
            Assert.AreEqual("do not remove of modify", user.FirstName);
            Assert.AreEqual("user for tests", user.LastName);
            Assert.NotNull(user.Token);
            Assert.False(user.EmailConfirmed);

            var user2 = User.Get(user.Token);
            Assert.AreEqual(user.FirstName, user2.FirstName);
            Assert.AreEqual(user.LastName, user2.LastName);
            Assert.AreEqual(user.Email, user2.Email);
            Assert.AreEqual(user.Username, user2.Username);
            Assert.AreEqual(user.Token, user2.Token);
        }

        [Test]
        public void UserReloadWithAppTokenSuccess()
        {
            var user = Login();
            user.Token = string.Empty;

            user.Reload();
        }

        [Test]
        public void UploadUserPhoto()
        {
            var user = Login();

            var path = Path.Combine(Path.GetTempPath(), $"{user.Id}_photo.png");
            const string content = "iVBORw0KGgoAAAANSUhEUgAAAQAAAAEAAQMAAABmvDolAAAAA1BMVEW10NBjBBbqAAAAH0lEQVRoge3BAQ0AAADCoPdPbQ43oAAAAAAAAAAAvg0hAAABmmDh1QAAAABJRU5ErkJggg==";
            var bytes = Convert.FromBase64String(content);
            File.WriteAllBytes(path, bytes);

            user.DeletePhoto();
            Assert.IsNull(user.PhotoLarge);
            Assert.IsNull(user.PhotoSmall);

            user.UploadPhoto(path);
            Assert.IsNotNull(user.PhotoLarge);
            Assert.IsNotNull(user.PhotoSmall);

            File.Delete(path);
        }

        private static User Login()
        {
            var user = User.Login("support@ivelum.com", "SJW8Gg");
            return user;
        }
    }
}
