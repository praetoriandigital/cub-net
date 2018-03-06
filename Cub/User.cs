using System;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

namespace Cub
{
    public class User : CObject
    {
        public User()
        {
        }

        public User(User obj)
            : base(obj)
        {
            Properties["email_confirmed"] = false;
        }

        public override string InstanceUrl => "user";

        public DateTime? BirthDate => _nullableValue<DateTime>("birth_date");

        public DateTime? DateJoined => _nullableValue<DateTime>("date_joined");

        public string RegistrationSite => _string("registration_site");

        public string PhotoLarge => _string("photo_large");

        public string PhotoSmall => _string("photo_small");

        public bool? InvalidEmail => _nullableValue<bool>("invalid_email");

        public bool? PurchasingRoleBuyForOrganization => _nullableValue<bool>("purchasing_role_buy_for_organization");

        public bool? PurchasingRoleBuyForSelfOnly => _nullableValue<bool>("purchasing_role_buy_for_self_only");

        public bool? PurchasingRoleRecommend => _nullableValue<bool>("purchasing_role_recommend");

        public bool? PurchasingRoleSpecifyForOrganization => _nullableValue<bool>("purchasing_role_specify_for_organization");

        public ICollection<string> Membership => _list<string>("membership");

        public ICollection<string> VerifiedTags => _list<string>("verified_tags");

        #region Read/Write properties

        public string FirstName
        {
            get => _string("first_name");
            set => Properties["first_name"] = value;
        }

        public string LastName
        {
            get => _string("last_name");
            set => Properties["last_name"] = value;
        }

        public string Email
        {
            get => _string("email");
            set => Properties["email"] = value;
        }

        public bool EmailConfirmed
        {
            get => _value<bool>("email_confirmed");
            set => Properties["email_confirmed"] = value;
        }

        public string Username
        {
            get => _string("username");
            set => Properties["username"] = value;
        }

        public string MiddleName
        {
            get => _string("middle_name");
            set => Properties["middle_name"] = value;
        }

        public string Gender
        {
            get => _string("gender");
            set => Properties["gender"] = value;
        }

        public string Token
        {
            get => ApiKey;
            set => ApiKey = value;
        }

        #endregion

        #region Methods

        public static User Login(string username, string password)
        {
            var postData = JObject.FromObject(new
            {
                username,
                password,
            });
            var user = BasePost<User>("user/login", postData);
            user.Token = user._string("token");
            return user;
        }

        public static User Get(string token)
        {
            return BaseGet<User>(null, token);
        }

        public static User GetByUid(string id)
        {
            return BaseGet<User>(id, null);
        }

        public User Reload()
        {
            BaseReload(string.IsNullOrEmpty(Token) ? base.InstanceUrl : InstanceUrl);
            return this;
        }

        public void UploadPhoto(string filename)
        {
            Api.UploadImage(filename, "user/photo", Token);
            Reload();
        }

        public void DeletePhoto()
        {
            Api.DeleteImage("user/photo", Token);
            Reload();
        }

        #endregion
    }
}
