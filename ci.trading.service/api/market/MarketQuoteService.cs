using ci.trading.models.app;
using ci.trading.models.marketquote;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ci.trading.service.api.market
{
    public class MarketQuoteService : IMarketQuoteService
    {
        private readonly ILogger<MarketQuoteService> _logger;
        private readonly AppSettings _appSettings;
        private string QUOTE_URL = "https://api.tradeking.com/v1/market/ext/quotes.json?symbols=";

        public MarketQuoteService(
            ILogger<MarketQuoteService> logger,
            IOptions<AppSettings> appSettings
            )
        {
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        public async Task<MarketQuoteModel> CallApi(HttpClient httpClient, List<string> symbolList)
        {
            var symbolString = Utils.GetCommaStringFromList(symbolList);
            QUOTE_URL += symbolString;
            Utils.SetupApiCall(_appSettings, QUOTE_URL, "GET", httpClient);
            var marketQuoteModel = new MarketQuoteModel();

            try
            {
                var response = await httpClient.GetAsync(QUOTE_URL);
                var data = await response.Content.ReadAsStringAsync();
                marketQuoteModel = ParseResponse(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in AccountService.CallApi: {ex}");
            }

            return marketQuoteModel;
        }

        public MarketQuoteModel ParseResponse(string data)
        {
            return new MarketQuoteModel();
        }
    }
        
}
