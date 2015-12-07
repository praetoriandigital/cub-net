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

        public string Subject
        {
            get { return _string("subject"); }
            set { Properties["subject"] = value; }
        }

        public int TotalBounces
        {
            get { return _value<int>("total_bounces"); }
        }

        public int TotalDelivered
        {
            get { return _value<int>("total_delivered"); }
        }

        public int TotalOpens
        {
            get { return _value<int>("total_opens"); }
        }

        public int UniqueOpens
        {
            get { return _value<int>("unique_opens"); }
        }

        public string FCDB
        {
            get { return _string("fc_db"); }
            set { Properties["fc_db"] = value; }
        }
    
        public int? FCNewsletterID
        {
            get { return _nullableValue<int>("fc_newsletter_id"); }
            set { Properties["fc_newsletter_id"] = value; }
        }

        public DateTime? Created
        {
            get { return _nullableValue<DateTime>("created"); }
        }
        #region Methods

        public static Message Get(string id, string apiKey)
        {
            return BaseGet<Message>(id, apiKey);
        }

        public static Message Get(string id)
        {
            return BaseGet<Message>(id, null);
        }

        public static List<Message> List(Dictionary<string, object> filters, string apiKey)
        {
            return BaseList<Message>(filters, apiKey);
        }

        public static List<Message> List(Dictionary<string, object> filters)
        {
            return BaseList<Message>(filters, null);
        }

        public static List<Message> List(string apiKey)
        {
            return BaseList<Message>(null, apiKey);
        }

        public static List<Message> List()
        {
            return BaseList<Message>(null, null);
        }


        #endregion
    }
}
