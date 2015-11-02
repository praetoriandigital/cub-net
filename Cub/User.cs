using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Cub
{
    public class User : CObject
    {
        public User() { }

        public User(User obj)
            : base(obj)
        {
            Properties["email_confirmed"] = false;
        }

        public override string InstanceUrl
        {
            get { return "user"; }
        }

        #region Read/Write properties

        public string FirstName
        {
            get { return _string("first_name"); }
            set { Properties["first_name"] = value; }
        }

        public string LastName
        {
            get { return _string("last_name"); }
            set { Properties["last_name"] = value; }
        }

        public string Email
        {
            get { return _string("email"); }
            set { Properties["email"] = value; }
        }

        public bool EmailConfirmed
        {
            get { return _value<bool>("email_confirmed"); }
            set { Properties["email_confirmed"] = value; }
        }

        public string Username
        {
            get { return _string("username"); }
            set { Properties["username"] = value; }
        }

        public string Token
        {
            get { return ApiKey; }
            set { ApiKey = value; }
        }

        #endregion

        #region Methods

        public static User Login(string username, string password)
        {
            var postData = JObject.FromObject(new
            {
                username = username,
                password = password,
            });
            var user = BasePost<User>("user/login", postData);
            user.Token = user._string("token");
            return user;
        }

        public static User Get(string token)
        {
            return BaseGet<User>(null, token);
        }

        public User Reload()
        {
            BaseReload();
            return this;
        }

        #endregion
    }
}
