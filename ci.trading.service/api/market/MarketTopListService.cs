using ci.trading.models.app;
using ci.trading.models.markettoplist;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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

        public async Task<List<MarketTopList>> CallApi(HttpClient httpClient, TopListType topListType, ExchangeType exchange = ExchangeType.N)
        {
            var endpoint = $"{TOP_LIST_URL}{topListType}.json?exchange={exchange}";
            Utils.SetupApiCall(_appSettings, endpoint, "GET", httpClient);
            var marketTopList = new List<MarketTopList>();

            try
            {
                var response = await httpClient.GetAsync(endpoint);
                var data = await response.Content.ReadAsStringAsync();
                marketTopList = ParseResponse(data, topListType);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error in MarketTopListService.CallApi: {ex.ToString()}");
            }

            return marketTopList;
        }

        private List<MarketTopList> ParseResponse(string data, TopListType topListType)
        {
            var listMarketTopList = new List<MarketTopList>();

            try
            {
                dynamic dynamicResponse = JsonConvert.DeserializeObject(data);
                var response = dynamicResponse.response;

                if(response.error == "Success")
                {
                    var quotes = response.quotes.quote;
                    foreach(var quote in quotes.Children())
                    {
                        var marketTopList = new MarketTopList
                        {
                            ResponseId = quote.ResponseId = response["@id"] ?? "",
                            TopListType = topListType,
                            Change = quote.chg,
                            ChangeType = quote.chg_sign,
                            Last = quote.last,
                            CompanyName = quote.name,
                            PercentChange = quote.pchg,
                            PriorDayClose = quote.pcls,
                            Rank = quote.rank,
                            Symbol = quote.symbol,
                            Volume = quote.vl
                        };

                        listMarketTopList.Add(marketTopList);
                    }
                }
                else
                {
                    var marketTopListError = new List<MarketTopList>
                    {
                        new MarketTopList
                        {
                            IsSuccessful = false,
                            Error = response.error
                        }
                    };

                return marketTopListError;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error in MarketTopListService.ParseResponse: {ex.ToString()}");
            }

            return listMarketTopList;
        }
    }
}
