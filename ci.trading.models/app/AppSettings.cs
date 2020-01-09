using System;
using System.Collections.Generic;
using System.Text;

namespace ci.trading.models.app
{
    public class AppSettings
    {
        public string ApiEndpoint { get; set; }
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string TokenKey { get; set; }
        public string TokenSecret { get; set; }

        public AppSettings()
        {

        }
    }
}
