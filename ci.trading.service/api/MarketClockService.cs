using ci.trading.models.app;
using ci.trading.models.marketclock;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ci.trading.service.api
{
    public class MarketClockService : IMarketClockService
    {
        private readonly ILogger<MarketClockService> _logger;
        private readonly AppSettings _appSettings;
        private const string MARKET_CLOCK_URL = "https://api.tradeking.com/v1/market/clock.json";

        public MarketClockService(
            ILogger<MarketClockService> logger,
            IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        public async Task<MarketClockModel> CallApi(HttpClient httpClient)
        {
            Utils.SetupApiCall(_appSettings, MARKET_CLOCK_URL, "GET", httpClient);
            var marketClockModel = new MarketClockModel();

            try
            {
                var response = await httpClient.GetAsync(MARKET_CLOCK_URL);
                var data = await response.Content.ReadAsStringAsync();
                marketClockModel = ParseResponse(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in AccountService.CallApi: {ex}");
            }

            return marketClockModel;
        }

        public MarketClockModel ParseResponse(string data)
        {
            throw new NotImplementedException();
        }
    }
}
