using ci.trading.models.marketclock;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ci.trading.service.api
{
    public interface IMarketClockService
    {
        Task<MarketClockModel> CallApi(HttpClient httpClient);
        MarketClockModel ParseResponse(string data);
    }
}
