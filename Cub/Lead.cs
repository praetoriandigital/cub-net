using System;

namespace Cub
{
    public class Lead : CObject
    {
        public Lead()
        {
        }

        public Lead(Lead obj)
            : base(obj)
        {
        }

        public DateTime Created => _value<DateTime>("user");

        public string Email => _string("email");

        public LeadData Data => _refType<LeadData>("data");

        public string RemoteIp => _string("remote_ip");

        public string Site => _string("site");

        public string Url => _string("url");

        public string User => _string("user");
    }
}
