using ci.trading.models.app;
using System;
using System.Collections.Generic;
using System.Text;

namespace ci.trading.models.account
{
    public class AccountModel : ApiBase
    {
        public string AccountId { get; set; }
        public decimal AccountValue { get; set; }
        public decimal CashAvailable { get; set; }
        public decimal OptionValue { get; set; }
        public decimal StockValue { get; set; }
        public decimal UnsettledFunds { get; set; }
        public decimal UnclearedDeposits { get; set; }
        public decimal BuyingPower { get; set; }
    }
}
