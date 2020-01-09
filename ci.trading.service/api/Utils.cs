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
    }

}
