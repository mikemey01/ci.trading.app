using ci.trading.models.app;
using System;
using System.Collections.Generic;
using System.Text;

namespace ci.trading.models.marketquote
{
    public class MarketQuoteModel : ApiBase
    {
        public string QuoteType { get; set; }
        public decimal AverageDailyPrice100 { get; set; }
        public decimal AverageDailyPrice200 { get; set; }
        public decimal AverageDailyPrice50 { get; set; }
        public decimal AverageDailyVolume21 { get; set; }
        public decimal AverageDailyVolume30 { get; set; }
        public decimal AverageDailyVolume90 { get; set; }
        public decimal AskPrice { get; set; }
        public decimal AskSize { get; set; } // in hundreds
        public decimal Bid { get; set; }
        public decimal BidSize { get; set; } // in hundreds
        public int MyProperty { get; set; }

    }
}
