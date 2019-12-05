using System;
using System.Configuration;

namespace Cub
{
    public static class Config
    {
        private static string _apiKey;

        public static string ApiKey
        {
            get {
                var key = _apiKey;
                if (string.IsNullOrEmpty(key)) {
                    key = Environment.GetEnvironmentVariable("INTEGRATION_TESTS_SECRET_KEY");
                }
                if (string.IsNullOrEmpty(key)) {
                    key = ConfigurationManager.AppSettings["CubApiKey"];
                }
                return key;
            }
            set => _apiKey = value;
        }

        public const string DefaultApiUrl = "https://id.lexipol.com/v1/";

        private static string _apiUrl;

        public static string ApiUrl
        {
            get
            {
                var configUrl = ConfigurationManager.AppSettings["CubApiUrl"];
                var defaultUrl = string.IsNullOrEmpty(configUrl) ? DefaultApiUrl : configUrl;
                var apiUrl = string.IsNullOrEmpty(_apiUrl) ? defaultUrl : _apiUrl;
                return apiUrl.EndsWith("/") ? apiUrl : apiUrl + "/";
            }
            set => _apiUrl = value;
        }

    }
}
