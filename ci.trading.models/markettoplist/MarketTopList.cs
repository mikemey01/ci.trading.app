using System;
using System.Collections.Generic;
using System.Text;

namespace ci.trading.models.markettoplist
{
    public class MarketTopList
    {
        public TopListType TopListType { get; set; }
        public decimal Change { get; set; }
        public decimal Last { get; set; }
        public string CompanyName { get; set; }
        public decimal PercentChange { get; set; }
        public decimal PriorDayClose { get; set; }
        public int Rank { get; set; }
        public string Symbol { get; set; }
        public decimal Volume { get; set; }
    }
}
