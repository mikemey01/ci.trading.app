using ci.trading.models.app;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ci.trading.service.api
{
    public static class Utils
    {
        private const string REQUEST_TOKEN_URL = "https://developers.tradeking.com/oauth/request_token";
        private const string ACCESS_TOKEN_URL = "https://developers.tradeking.com/oauth/access_token";

        public static HttpClient SetupApiCall(AppSettings apiSettings, string url, string type, HttpClient httpClient)
        {
            var oauth = new OAuth.Manager();

            oauth["consumer_key"] = apiSettings.ConsumerKey;
            oauth["consumer_secret"] = apiSettings.ConsumerSecret;
            oauth["token"] = apiSettings.TokenKey;
            oauth["token_secret"] = apiSettings.TokenSecret;

            var header = oauth.GenerateAuthzHeader(url, type);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", header);

            return httpClient;
        }

        public static string GetCommaStringFromList(List<string> list)
        {
            var stringList = string.Join(",", list);
            return stringList;
        }

        public static DateTime? ParseYYYYMMDDDate(string date)
        {
            if (date.Length != 8) return null;

            try
            {
                var year = date.Substring(0, 4);
                var month = date.Substring(4, 2);
                var day = date.Substring(6, 2);
                var dateTime = new DateTime(Convert.ToInt32(date.Substring(0, 4)), Convert.ToInt32(date.Substring(4, 2)), Convert.ToInt32(date.Substring(6, 2)));
                return dateTime;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public static string GetStringDateTime(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        public static TimeSpan GetMarketOpenTime()
        {
            return new TimeSpan(14, 30, 00);
        }

        public static TimeSpan GetMarketCloseTime()
        {
            return new TimeSpan(21, 00, 00);
        }
    }

}
