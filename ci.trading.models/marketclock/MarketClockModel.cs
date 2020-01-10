using ci.trading.models.app;
using System;
using System.Collections.Generic;
using System.Text;

namespace ci.trading.models.marketclock
{
    public class MarketClockModel : ApiBase
    {
        public DateTime CurrentDateTime { get; set; }
        public string CurrentMarketStatus { get; set; }
        public string NextMarketStatus { get; set; }
        public DateTime NextMarketStatusDateTime { get; set; }
        public string Message { get; set; }
        public double UnixTime { get; set; }
    }
}
