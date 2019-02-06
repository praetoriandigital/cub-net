using System;
using System.Collections.Generic;

namespace Cub
{
    public class Message : CObject
    {
        public Message() { }

        public Message(Message obj)
            : base(obj)
        {
        }

        public string Name
        {
            get => _string("name");
            set => Properties["name"] = value;
        }

        public string Subject
        {
            get => _string("subject");
            set => Properties["subject"] = value;
        }

        public int TotalBounces => _value<int>("total_bounces");

        public int TotalDelivered => _value<int>("total_delivered");

        public int TotalOpens => _value<int>("total_opens");

        public int UniqueOpens => _value<int>("unique_opens");

        public int ValidSubscribers => _value<int>("valid_subscribers");

        public int ReportedDeliveries => _value<int>("reported_deliveries");

        public string FcDb
        {
            get => _string("fc_db");
            set => Properties["fc_db"] = value;
        }
    
        public int? FcNewsletterId
        {
            get => _nullableValue<int>("fc_newsletter_id");
            set => Properties["fc_newsletter_id"] = value;
        }

        public DateTime? Created => _nullableValue<DateTime>("created");

        public DateTime? Sent => _nullableValue<DateTime>("sent_date");

        #region Methods

        public static Message Get(string id, string apiKey)
        {
            return BaseGet<Message>(id, apiKey);
        }

        public static Message Get(string id)
        {
            return BaseGet<Message>(id, null);
        }

        public static List<Message> List(Dictionary<string, object> filters, string apiKey, int offset, int count)
        {
            return BaseList<Message>(filters, apiKey, offset, count);
        }

        public static List<Message> List(Dictionary<string, object> filters, int offset, int count)
        {
            return BaseList<Message>(filters, null, offset, count);
        }

        public static List<Message> List(string apiKey, int offset, int count)
        {
            return BaseList<Message>(null, apiKey, offset, count);
        }

        public static List<Message> List(int offset, int count)
        {
            return BaseList<Message>(null, null, offset, count);
        }


        #endregion
    }
}
