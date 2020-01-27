using ci.trading.models.app;
using System;
using System.Collections.Generic;
using System.Text;

namespace ci.trading.models.markettimesales
{
    public class MarketCandle : ApiBase
    {
        public DateTime Date { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Last { get; set; }
        public decimal Volume { get; set; }
        public string Interval { get; set; }

    }
}
