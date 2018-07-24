using System;
using System.Collections.Generic;

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

        public DateTime Created => _value<DateTime>("created");

        public LeadData Data => _refType<LeadData>("data");

        public string Email => _string("email");

        public string LeadStatus => _string("lead_status");

        public string RemoteIp => _string("remote_ip");

        public string Site => _string("site");

        public DateTime? StatusUpdated => _nullableValue<DateTime>("status_updated");

        public string Url => _string("url");

        public string User => _string("user");

        public string Form => _string("form");

        public bool IsProduction => _value<bool>("production");

        public Organization Organization => _expandable("organization", Organization.Get);

        public static Lead Get(string id, string apiKey = null, string expand = "organization__country,organization__state")
        {
            var parameters = new Dictionary<string, object>
            {
                ["expand"] = "organization__country,organization__state",
            };
            return BaseGet<Lead>(id, apiKey, parameters);
        }

        public static List<Lead> List(DateTime? from = null, DateTime? to = null)
        {
            return BaseList<Lead>(PrepareFilters(from, to), null);
        }

        public static List<Lead> List(int offset, int count, DateTime? from = null, DateTime? to = null)
        {
            return BaseList<Lead>(PrepareFilters(from, to), null, offset, count);
        }

        private static Dictionary<string, object> PrepareFilters(DateTime? from, DateTime? to)
        {
            var filters = new Dictionary<string, object>();
            if (from.HasValue)
                filters["created__gte"] = Utils.UnixTimestamp(from.Value);
            if (to.HasValue)
                filters["created__lte"] = Utils.UnixTimestamp(to.Value);
            return filters;
        }
    }
}
