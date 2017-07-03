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

        public LeadData Data => _refType<LeadData>("data");

        public string Email => _string("email");

        public string LeadStatus => _string("lead_status");

        public string RemoteIp => _string("remote_ip");

        public string Site => _string("site");

        public DateTime? StatusUpdated => _nullableValue<DateTime>("status_updated");

        public string Url => _string("url");

        public string User => _string("user");
    }
}
