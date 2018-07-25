using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq.Expressions;

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
            return $"{className.ToLower()}s";
        }

        public static string ClassUrl(Type type)
        {
            return ClassUrl(type.Name);
        }

        public static string ClassUrl(CObject obj)
        {
            return ClassUrl(obj.GetType().Name);
        }

        public virtual string InstanceUrl => $"{ClassUrl(this)}/{Id}";

        public bool? Deleted => _nullableValue<bool>("deleted");

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

        protected CObject BaseReload(Dictionary<string, object> parameters = null)
        {
            return BaseReload(InstanceUrl, parameters);
        }

        protected CObject BaseReload(string url, Dictionary<string, object> parameters = null)
        {
            PopulateApiKey();
            return FromObject(Api.RequestObject("GET", url, parameters, ApiKey));
        }

        protected static T BaseGet<T>(string id, string apiKey, Dictionary<string, object> parameters = null) where T : CObject, new()
        {
            var obj = new T
            {
                Id = id,
                ApiKey = apiKey
            };
            obj.BaseReload(parameters);
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

        protected static List<T> BaseListAll<T>(Dictionary<string, object> parameters, string apiKey) where T : CObject, new()
        {
            var allObjects = new List<T>();
            var count = 100;
            var offset = 0;
            while (true)
            {
                var objects = BaseList<T>(parameters, apiKey, offset, count, 3);
                allObjects.AddRange(objects);

                if (objects.Count < count)
                    break;

                offset += count;
            }
            return allObjects;
        }

        protected static List<T> BaseList<T>(Dictionary<string, object> parameters, string apiKey, int offset = 0, int count = 20, int maxRetries = 1)
            where T : CObject, new()
        {
            parameters["offset"] = offset;
            parameters["count"] = count;
            var objects = new List<T>();
            var items = Api.RequestArray("GET", ClassUrl(typeof(T)), parameters, apiKey, maxRetries);
            foreach (var item in items)
            {
                T obj = new T();
                obj.PopulateApiKey(apiKey);
                obj.FromObject((JObject) item);
                objects.Add(obj);
            }

            return objects;
        }

        protected string _string(string propName)
        {
            return Properties[propName] == null ? null : Properties[propName].Value<string>();
        }

        protected T _refType<T>(string propName) where T : class
        {
            var data = Properties[propName]?.ToString();
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<T>(data);
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
            if (!(Properties[propName] is JArray arr))
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

    public class ExpandableCObject<T> : CObject where T : CObject
    {
        private readonly Dictionary<string, CObject> _expandedObjects = new Dictionary<string, CObject>();

        protected ExpandableCObject()
        {
        }

        protected ExpandableCObject(T obj) : base(obj)
        {
        }

        public T Reload()
        {
            return Reload(new Dictionary<string, object>());
        }

        public T Reload(params Expression<Func<T, object>>[] expands)
        {
            return Reload(new Dictionary<string, object>(), expands);
        }

        public T Reload(Dictionary<string, object> parameters, params Expression<Func<T, object>>[] expandExpressions)
        {
            var expands = ParseExpressions(expandExpressions);
            return Reload(parameters, expands);
        }

        private T Reload(Dictionary<string, object> parameters, IEnumerable<string> expands = null)
        {
            if (expands != null)
            {
                parameters = AddExpand(parameters, expands);
            }

            _expandedObjects.Clear();
            return BaseReload(parameters) as T;
        }

        protected TProp _expandable<TProp>(string propName) where TProp : CObject, new()
        {
            if (_expandedObjects.ContainsKey(propName))
            {
                return _expandedObjects[propName] as TProp;
            }

            var data = Properties[propName]?.ToString();
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            TProp obj;
            if (data.StartsWith("{"))
            {
                obj = new TProp().FromString(data) as TProp;
            }
            else
            {
                obj = new TProp
                {
                    Id = data,
                    ApiKey = ApiKey
                };
            }

            _expandedObjects[propName] = obj;
            return obj;
        }

        protected static List<string> ParseExpressions(Expression<Func<T, object>>[] expressions)
        {
            var expands = new List<string>();
            foreach (var expression in expressions)
            {
                var names = new List<string>();
                var ex = expression.Body;
                while (ex.NodeType != ExpressionType.Parameter)
                {
                    if (ex.NodeType != ExpressionType.MemberAccess)
                        throw new InvalidOperationException("Expression must be a MemberAccess expression.");

                    var member = (MemberExpression) ex;
                    var name = member.Member.Name.ToLower();
                    names.Insert(0, name);

                    ex = member.Expression;
                }

                var expand = string.Join("__", names.ToArray());
                expands.Add(expand);
            }

            return expands;
        }

        protected static Dictionary<string, object> AddExpand(Dictionary<string, object> parameters, IEnumerable<string> expands)
        {
            parameters = new Dictionary<string, object>(parameters ?? new Dictionary<string, object>())
            {
                ["expand"] = string.Join(",", new List<string>(expands).ToArray()),
            };
            return parameters;
        }
    }
}
