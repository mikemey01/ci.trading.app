using ci.trading.models.markettimesales;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ci.trading.service.api.market
{
    public interface IMarketTimeSalesService
    {
        Task<List<MarketDay>> CallApi(HttpClient httpClient, string interval, string symbol, DateTime startDate);
    }
}
