using System;
using System.Globalization;
using System.Web;
using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json.Linq;

namespace Cub
{
    public class Utils
    {
        public static string Urlify(string key, object value, string prefix)
        {
            var pfx = string.IsNullOrEmpty(prefix) ? "" : $"{prefix}__";

            if (value is JObject o)
            {
                var dict = new Dictionary<string, object>();
                foreach (var item in o.Children())
                {
                    if (item is JProperty)
                    {
                        var prop = item as JProperty;
                        dict.Add(prop.Name, prop.Value);
                    }
                }
                return Urlify(dict, $"{pfx}{key}");
            }

            if (value is IDictionary dictionary)
            {
                var dict = new Dictionary<string, object>();
                foreach (DictionaryEntry item in dictionary)
                {
                    dict.Add((string)item.Key, item.Value);
                }
                return Urlify(dict, $"{pfx}{key}");
            }

            if (value is IList list1)
            {
                var list = new List<object>();
                foreach (var item in list1)
                {
                    list.Add(item);
                }
                return Urlify(list, $"{pfx}{key}");
            }

            string type;
            object sourceValue;
            string valueStr;

            if (value is JValue jvalue)
            {
                type = jvalue.Value?.GetType().Name.ToLower() ?? "string";
                sourceValue = jvalue.Value;
            }
            else
            {
                type = value.GetType().Name.ToLower();
                sourceValue = value;
            }

            if (type == "decimal")
            {
                // ReSharper disable once PossibleNullReferenceException
                valueStr = ((decimal) sourceValue).ToString(CultureInfo.InvariantCulture);
            }
            else if (type == "double" || type == "float")
            {
                // ReSharper disable once PossibleNullReferenceException
                valueStr = ((double) sourceValue).ToString(CultureInfo.InvariantCulture);
            }
            else
                valueStr = value.ToString();

            return $"{pfx}{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(valueStr)}";
        }

        public static string Urlify(Dictionary<string, object> parameters, string prefix)
        {
            if (parameters == null)
                return "";
            var pairs = new List<string>();
            foreach (var p in parameters)
            {
                pairs.Add(Urlify(p.Key, p.Value, prefix));
            }
            return string.Join("&", pairs.ToArray());
        }

        public static string Urlify(List<object> parameters, string prefix)
        {
            var pairs = new List<string>();
            for (var i = 0; i < parameters.Count; i++)
            {
                pairs.Add(Urlify(i.ToString(), parameters[i], prefix));
            }
            return string.Join("&", pairs.ToArray());
        }

        public static string Urlify(Dictionary<string, object> parameters)
        {
            return Urlify(parameters, null);
        }

        public static string Urlify(JObject parameters, string prefix)
        {
            var pairs = new List<string>();
            foreach (var item in parameters.Children())
            {
                if (item is JProperty)
                {
                    var prop = item as JProperty;
                    pairs.Add(Urlify(prop.Name, prop.Value, prefix));
                }
            }
            return string.Join("&", pairs.ToArray());
        }

        public static string Urlify(JObject parameters)
        {
            return Urlify(parameters, null);
        }

        public static int UnixTimestamp(DateTime d)
        {
            return (int)d.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
