using ci.trading.models.app;
using ci.trading.models.markettoplist;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ci.trading.service.api.market
{
    public class MarketTopListService : IMarketTopListService
    {
        private readonly ILogger<MarketTopListService> _logger;
        private readonly AppSettings _appSettings;
        private string TOP_LIST_URL = "https://api.tradeking.com/v1/market/toplists/";

        public MarketTopListService(
            ILogger<MarketTopListService> logger,
            IOptions<AppSettings> appSettings
            )
        {
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        public async Task<List<MarketTopList>> CallApi(HttpClient httpClient, TopListType topListType)
        {
            var endpoint = $"{TOP_LIST_URL}{topListType}.json?exchange=N";
            Utils.SetupApiCall(_appSettings, endpoint, "GET", httpClient);
            var marketTopList = new List<MarketTopList>();

            try
            {
                var response = await httpClient.GetAsync(endpoint);
                var data = await response.Content.ReadAsStringAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error in MarketTopListService.CallApi: {ex.ToString()}");
            }

            return marketTopList;
        }
    }
}
