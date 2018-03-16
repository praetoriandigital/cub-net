using System;
using System.Collections.Generic;

namespace Cub
{
    public class ServiceSubscription : CObject
    {
        public ServiceSubscription()
        {
        }

        public ServiceSubscription(ServiceSubscription obj)
            : base(obj)
        {
        }

        public static ServiceSubscription Get(string id)
        {
            return BaseGet<ServiceSubscription>(id, null);
        }

        public static List<ServiceSubscription> List(Dictionary<string, object> filters)
        {
            return BaseList<ServiceSubscription>(filters, null);
        }

        private string CustomerUid => _string("customer");

        private string PlanUid => _string("plan");

        public Customer Customer => Customer.Get(CustomerUid);

        public Plan Plan => string.IsNullOrEmpty(PlanUid) ? null : Plan.Get(PlanUid);

        public DateTime? ActiveSince => _nullableValue<DateTime>("active_since");

        public DateTime? ActiveTill => _nullableValue<DateTime>("active_till");
    }
}
