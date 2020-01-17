using ci.trading.models.marketquote;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ci.trading.service.api.market
{
    public interface IMarketQuoteService
    {
        Task<MarketQuoteModel> CallApi(HttpClient httpClient, List<string> symbolList);
        MarketQuoteModel ParseResponse(string data);
    }
}
