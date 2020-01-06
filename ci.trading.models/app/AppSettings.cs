using System;
using System.Collections.Generic;
using System.Text;

namespace ci.trading.models.app
{
    public class AppSettings
    {
        public string ApiPaperEndpoint { get; set; }
        public string ApiPaperKey { get; set; }
        public string ApiLiveEndpoint { get; set; }
        public string ApiLiveKey { get; set; }

        public AppSettings()
        {

        }
    }
}
