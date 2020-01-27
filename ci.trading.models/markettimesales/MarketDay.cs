using ci.trading.models.app;
using System;
using System.Collections.Generic;
using System.Text;

namespace ci.trading.models.markettimesales
{
    public class MarketDay : ApiBase
    {
        public DateTime Date { get; set; }
        public List<MarketCandle> Candles { get; set; }
        public string interval { get; set; }
    }
}
