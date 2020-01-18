using ci.trading.models.app;
using ci.trading.models.marketquote;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public async Task<List<MarketQuoteModel>> CallApi(HttpClient httpClient, List<string> symbolList)
        {
            var symbolString = Utils.GetCommaStringFromList(symbolList);
            QUOTE_URL += symbolString;
            Utils.SetupApiCall(_appSettings, QUOTE_URL, "GET", httpClient);
            var marketQuoteModels = new List<MarketQuoteModel>();

            try
            {
                var response = await httpClient.GetAsync(QUOTE_URL);
                var data = await response.Content.ReadAsStringAsync();

                // if we're parsing multiple symbols, get the list back.
                if(symbolList.Count > 1)
                {
                    marketQuoteModels = ParseMultipleQuotes(data);
                }
                else
                {
                    // parse a single symbol
                    var marketQuote = ParseSingleQuote(data);
                    marketQuoteModels.Add(marketQuote);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in MarketQuoteService.CallApi: {ex.ToString()}");
            }

            return marketQuoteModels;
        }

        private MarketQuoteModel ParseSingleQuote(string data)
        {
            var marketQuoteModel = new MarketQuoteModel();
            try
            {
                dynamic dynamicResponse = JsonConvert.DeserializeObject(data);
                var response = dynamicResponse.response;
                var quote = response.quotes.quote;

                if (response.error == "Success")
                {
                    marketQuoteModel = ParseQuote(quote);
                }
                else
                {
                    marketQuoteModel.IsSuccessful = false;
                    marketQuoteModel.Error = response.error;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error in MarketQuoteModel.ParseSingleQuote: {ex.ToString()}");
            }

            return marketQuoteModel;
        }

        private List<MarketQuoteModel> ParseMultipleQuotes(string data)
        {
            var marketQuoteModels = new List<MarketQuoteModel>();
            try
            {
                dynamic dynamicResponse = JsonConvert.DeserializeObject(data);
                var response = dynamicResponse.response;
                var quotes = response.quotes.quote;

                if (response.error == "Success")
                {
                    foreach (var quote in quotes.Children())
                    {
                        var marketQuote = ParseQuote(quote); 
                        marketQuoteModels.Add(marketQuote);
                    }
                }
                else
                {
                    var marketQuote = new MarketQuoteModel
                    {
                        IsSuccessful = false,
                        Error = response.error
                    };
                    marketQuoteModels.Add(marketQuote);
                }                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in MarketQuoteModel.ParseMultipleQuotes: {ex.ToString()}");
            }

            return new List<MarketQuoteModel>();
        }

        private MarketQuoteModel ParseQuote(dynamic quote)
        {
            var marketQuoteModel = new MarketQuoteModel
            {
                AverageDailyPrice100 = quote.adp_100,
                AverageDailyPrice200 = quote.adp_200
            };

            return marketQuoteModel;
        }
    }
        
}
