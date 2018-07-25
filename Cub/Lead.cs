using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Cub
{
    public class Lead : ExpandableCObject<Lead>
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

        public Organization Organization => _expandable<Organization>("organization");

        public static List<Lead> List(int offset = 0, int count = 20, DateTime? from = null, DateTime? to = null, params Expression<Func<Lead, object>>[] expandExpressions)
        {
            var expands = ParseExpressions(expandExpressions);
            return BaseList<Lead>(PrepareParameters(from, to, expands), null, offset, count);
        }

        private static Dictionary<string, object> PrepareParameters(DateTime? from, DateTime? to, IEnumerable<string> expands)
        {
            var parameters = new Dictionary<string, object>();
            if (from.HasValue)
                parameters["created__gte"] = Utils.UnixTimestamp(from.Value);
            if (to.HasValue)
                parameters["created__lte"] = Utils.UnixTimestamp(to.Value);
            if (expands != null)
                parameters = AddExpand(parameters, expands);
            return parameters;
        }
    }
}
