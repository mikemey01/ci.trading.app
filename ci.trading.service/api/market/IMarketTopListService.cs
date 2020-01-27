using ci.trading.models.markettoplist;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ci.trading.service.api.market
{
    public interface IMarketTopListService
    {
        Task<List<MarketTopList>> CallApi(HttpClient httpClient, TopListType topListType);
    }
}
