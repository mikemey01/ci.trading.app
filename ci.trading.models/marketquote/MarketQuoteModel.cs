using ci.trading.models.app;
using System;
using System.Collections.Generic;
using System.Text;

namespace ci.trading.models.marketquote
{
    public class MarketQuoteModel : ApiBase
    {
        public decimal AverageDailyPrice100 { get; set; }
        public decimal AverageDailyPrice200 { get; set; }
        public decimal AverageDailyPrice50 { get; set; }
        public decimal AverageDailyVolume21 { get; set; }
        public decimal AverageDailyVolume30 { get; set; }
        public decimal AverageDailyVolume90 { get; set; }
        public decimal AskPrice { get; set; }
        public decimal AskSize { get; set; } // in hundreds
        public decimal BidPrice { get; set; }
        public decimal BidSize { get; set; } // in hundreds
        public decimal BetaVolatility { get; set; }
        public decimal PreviousCandleClose { get; set; }
        public decimal Dividend { get; set; }
        public DateTime? DividendExDate { get; set; }
        public decimal EarningsPerShare { get; set; }
        public decimal High { get; set; } // day high price
        public decimal LastTradeVolume { get; set; }
        public decimal Last { get; set; }
        public decimal Low { get; set; } //day low price
        public string Name { get; set; }
        public decimal Open { get; set; } // day open price
        public decimal PriorDayClose { get; set; }
        public decimal PriorDayHigh { get; set; }
        public decimal PriorDayLow { get; set; }
        public decimal PriorDayOpen { get; set; }
        public decimal PriorDayVolume { get; set; }
        public decimal PriceEarnings { get; set; }
        public decimal SharesOutstanding { get; set; }
        public string Symbol { get; set; }
        public decimal TradesSinceMarketOpen { get; set; }
        public string TrendTenTicks { get; set; }
        public decimal Volume { get; set; }
        public decimal VolatilityOneYear { get; set; }
        public decimal VolumeWeightedAveragePrice { get; set; }
        public decimal High52Week { get; set; }
        public DateTime? High52WeekDate { get; set; }
        public decimal Low52Week { get; set; }
        public DateTime? Low52WeekDate { get; set; }
        public decimal DividendYieldAsPercent { get; set; }

    }
}
