using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Cub
{
    public class CObject
    {
        public string Id { get; set; }

        public JObject Properties = new JObject();

        public string ApiKey { get; set; }

        public CObject() { }

        public CObject FromObject(CObject obj)
        {
            Id = obj.Id;
            Properties = new JObject(obj.Properties);
            ApiKey = obj.ApiKey;
            return this;
        }

        public CObject FromObject(JObject obj)
        {
            Id = obj.SelectToken("id").ToString();
            Properties = new JObject(obj);
            return this;
        }

        public CObject FromString(string jsonData)
        {
            return FromObject(JObject.Parse(jsonData));
        }

        public CObject(CObject obj)
        {
            FromObject(obj);
        }

        public CObject(JObject obj)
        {
            FromObject(obj);
        }

        public static string ClassUrl(string className)
        {
            return String.Format("{0}s", className.ToLower());
        }

        public static string ClassUrl(Type type)
        {
            return ClassUrl(type.Name);
        }

        public static string ClassUrl(CObject obj)
        {
            return ClassUrl(obj.GetType().Name);
        }

        public virtual string InstanceUrl
        {
            get { return String.Format("{0}/{1}", ClassUrl(this), Id); }
        }

        protected void PopulateApiKey()
        {
            if (string.IsNullOrEmpty(ApiKey))
                ApiKey = Config.ApiKey;
        }

        protected void PopulateApiKey(string apiKey)
        {
            ApiKey = apiKey;
            PopulateApiKey();
        }

        protected CObject BaseReload()
        {
            return BaseReload(InstanceUrl);
        }

        protected CObject BaseReload(string url)
        {
            PopulateApiKey();
            return FromObject(Api.RequestObject("GET", url, ApiKey));
        }

        protected static T BaseGet<T>(string id, string apiKey) where T : CObject, new()
        {
            var obj = new T();
            obj.Id = id;
            obj.ApiKey = apiKey;
            obj.BaseReload();
            return obj;
        }

        protected CObject BasePost(string url, JObject properties)
        {
            PopulateApiKey();
            return FromObject(Api.RequestObject("POST", url, properties, ApiKey));
        }

        public static T BasePost<T>(string url, JObject properties) where T : CObject, new()
        {
            var obj = new T();
            obj.BasePost(url, properties);
            return obj;
        }

        protected CObject BaseSave()
        {
            var saveUrl = string.IsNullOrEmpty(Id) ? ClassUrl(this) : InstanceUrl;
            return BasePost(saveUrl, Properties);
        }

        protected static T BaseCreate<T>(T obj, string apiKey) where T : CObject, new()
        {
            obj.PopulateApiKey(apiKey);
            obj.BaseSave();
            return obj;
        }

        protected CObject BaseDelete()
        {
            PopulateApiKey();
            return FromObject(Api.RequestObject("DELETE", InstanceUrl, ApiKey));
        }

        protected static List<T> BaseList<T>(Dictionary<string, object> filters, string apiKey) where T : CObject, new()
        {
            var items = Api.RequestArray("GET", ClassUrl(typeof(T)), filters, apiKey);
            var objects = new List<T>();
            foreach (var item in items)
            {
                T obj = new T();
                obj.PopulateApiKey(apiKey);
                obj.FromObject((JObject)item);
                objects.Add(obj);
            }
            return objects;
        }

        protected string _string(string propName)
        {
            return Properties[propName] == null ? (string)null : Properties[propName].Value<string>();
        }

        protected T _value<T>(string propName) where T : struct
        {
            return Properties[propName].Value<T>();
        }

        protected T? _nullableValue<T>(string propName) where T : struct
        {
            if (Properties[propName] == null || string.IsNullOrEmpty(Properties[propName].ToString()))
                return null;
            return Properties[propName].Value<T>();
        }

        protected List<T> _list<T>(string propName)
        {
            var arr = Properties[propName] as JArray;
            if (arr == null)
                return null;

            var res = new List<T>();
            foreach (var item in arr.Values<T>())
            {
                res.Add(item);
            }
            return res;
        }

        public override string ToString()
        {
            return Properties.ToString();
        }
    }
}
