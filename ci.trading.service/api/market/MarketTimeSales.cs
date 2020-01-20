using ci.trading.models.app;
using ci.trading.models.markettimesales;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ci.trading.service.api.market
{
    public class MarketTimeSales : IMarketTimeSales
    {
        private readonly ILogger<MarketQuoteService> _logger;
        private readonly AppSettings _appSettings;
        private string TIME_SALES_URL = "https://api.tradeking.com/v1/market/timesales.json?";

        public MarketTimeSales(
            ILogger<MarketQuoteService> logger,
            IOptions<AppSettings> appSettings
            )
        {
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        public async Task<List<MarketCandle>> CallApi(HttpClient httpClient, string interval, string symbol, DateTime startDate)
        {
            var endpoint = $"{TIME_SALES_URL}symbols={symbol}&startdate={Utils.GetStringDateTime(startDate)}&interval={interval}";
            Utils.SetupApiCall(_appSettings, endpoint, "GET", httpClient);
            var candleList = new List<MarketCandle>();
            try
            {
                var response = await httpClient.GetAsync(endpoint);
                var data = await response.Content.ReadAsStringAsync();
                candleList = ParseResponse(data);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error in MarketTimeSales.CallApi: {ex.ToString()}");
            }

            return candleList;
        }

        private List<MarketCandle> ParseResponse(string data)
        {
            return new List<MarketCandle>();
        }
    }
}
