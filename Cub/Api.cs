using System.Diagnostics;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Cub
{
    public class Api
    {
        public static string Request(string method, string url, string parameters, string apiKey)
        {
            // Make request URL and request object
            var reqUrl = Config.ApiUrl + url;
            if (method.ToLower() == "get")
                reqUrl += $"?{parameters}";
            var req = (HttpWebRequest)WebRequest.Create(reqUrl);
            req.Method = method;
            req.ContentType = "application/x-www-form-urlencoded";

            // Add auth
            var reqKey = string.IsNullOrEmpty(apiKey) ? Config.ApiKey : apiKey;
            req.Headers.Add("Authorization", $"Bearer {reqKey}");

            // Add system information
            req.UserAgent =
                $"Cub Client for .Net v{FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion}";
            var sysInfo = new JObject
            {
                ["publisher"] = "Cub",
                ["platform"] = Environment.OSVersion.ToString(),
                ["language"] = $"CLR {Environment.Version}"
            };
            req.Headers.Add("X-Cub-User-Agent-Info", sysInfo.ToString(Formatting.None));

            // Add parameters for requests other than GET
            if (method.ToLower() != "get")
            {
                byte[] bytes = Encoding.UTF8.GetBytes(parameters);
                req.ContentLength = bytes.Length;
                using (Stream st = req.GetRequestStream())
                {
                    st.Write(bytes, 0, bytes.Length);
                }
            }

            // Send request and handle errors
            try
            {
                using (var response = req.GetResponse())
                {
                    return ReadStream(response.GetResponseStream());
                }
            }
            catch (WebException webException)
            {
                if (webException.Response != null)
                {
                    var statusCode = ((HttpWebResponse)webException.Response).StatusCode;
                    var unipagError = JsonConvert<Error>.ConvertJsonToObject(ReadStream(webException.Response.GetResponseStream()), "error");

                    throw new CubExceptionFactory().Exception(unipagError, statusCode);
                }

                throw;
            }
        }

        private static string ReadStream(Stream stream)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        public static string Request(string method, string url, Dictionary<string, object> parameters, string apiKey)
        {
            return Request(method, url, Utils.Urlify(parameters), apiKey);
        }

        public static string Request(string method, string url, JObject parameters, string apiKey)
        {
            return Request(method, url, Utils.Urlify(parameters), apiKey);
        }

        public static JObject RequestObject(string method, string url, Dictionary<string, object> parameters, string apiKey)
        {
            string response = Request(method, url, parameters, apiKey);
            var obj = JObject.Parse(response);
            return obj;
        }

        public static JObject RequestObject(string method, string url, Dictionary<string, object> parameters)
        {
            return RequestObject(method, url, parameters, Config.ApiKey);
        }

        public static JObject RequestObject(string method, string url, JObject parameters, string apiKey)
        {
            string response = Request(method, url, parameters, apiKey);
            var obj = JObject.Parse(response);
            return obj;
        }

        public static JObject RequestObject(string method, string url, JObject parameters)
        {
            return RequestObject(method, url, parameters, Config.ApiKey);
        }

        public static JObject RequestObject(string method, string url, string apiKey)
        {
            return RequestObject(method, url, new Dictionary<string, object>(), apiKey);
        }

        public static JObject RequestObject(string method, string url)
        {
            return RequestObject(method, url, new Dictionary<string, object>(), Config.ApiKey);
        }

        public static JArray RequestArray(string method, string url, Dictionary<string, object> parameters, string apiKey)
        {
            string response = Request(method, url, parameters, apiKey);
            var obj = JArray.Parse(response);
            return obj;
        }

        public static JArray RequestArray(string method, string url, Dictionary<string, object> parameters)
        {
            return RequestArray(method, url, parameters, Config.ApiKey);
        }

        public static JArray RequestArray(string method, string url)
        {
            return RequestArray(method, url, new Dictionary<string, object>(), Config.ApiKey);
        }

        public static JArray RequestArray(string method, string url, string apiKey)
        {
            return RequestArray(method, url, new Dictionary<string, object>(), apiKey);
        }

        public static string RequestUploadFile(string url, string fileName, Dictionary<string, string> parameters,
            string contentType = "image/png", string paramName = "file")
        {
            var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            request.KeepAlive = true;
            
            using (var stream = request.GetRequestStream())
            {
                var boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                
                foreach (var key in parameters.Keys)
                {
                    stream.Write(boundaryBytes, 0, boundaryBytes.Length);
                    var item = $"Content-Disposition: form-data; name=\"{key}\"\r\n\r\n{parameters[key]}";
                    var data = Encoding.UTF8.GetBytes(item);
                    stream.Write(data, 0, data.Length);
                }
                stream.Write(boundaryBytes, 0, boundaryBytes.Length);

                var name = Path.GetFileName(fileName);
                var header = "Content-Disposition: form-data; " +
                             $"name=\"{paramName}\"; " +
                             $"filename=\"{name}\"\r\n" +
                             $"Content-Type: {contentType}\r\n\r\n";
                var headerBytes = Encoding.UTF8.GetBytes(header);
                stream.Write(headerBytes, 0, headerBytes.Length);

                using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    var buffer = new byte[4096];
                    int bytesRead;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        stream.Write(buffer, 0, bytesRead);
                    }
                }

                var trailer = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                stream.Write(trailer, 0, trailer.Length);
            }

            using (var response = request.GetResponse())
            {
                return ReadStream(response.GetResponseStream());
            }
        }

        public static void UploadImage(string filename, string url, string apiKey)
        {
            // Get credentials for uploading to S3
            var parameters = new Dictionary<string, object>
            {
                ["filename"] = Path.GetFileName(filename)
            };
            var res = RequestObject("GET", url, parameters, apiKey);
            var upploadUrl = (string) res["action"];
            var fields = res["fields"];
            var formData = new Dictionary<string, string>();
            foreach (var field in fields)
            {
                formData[field["name"].Value<string>()] = field["value"].Value<string>();
            }

            // Upload to S3
            RequestUploadFile(upploadUrl, filename, formData);
            RequestObject("POST", url, parameters, apiKey);
        }

        public static void DeleteImage(string url, string apiKey)
        {
            RequestObject("DELETE", url, apiKey);
        }
    }
}
