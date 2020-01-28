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
                        var marketQuote = (MarketQuoteModel)ParseQuote(quote); 
                        marketQuote.ResponseId = response["@id"] ?? "";
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

            return marketQuoteModels;
        }

        private MarketQuoteModel ParseQuote(dynamic quote)
        {
            try
            {
                var marketQuoteModel = new MarketQuoteModel
                {
                    AverageDailyPrice100 = quote.adp_100,
                    AverageDailyPrice200 = quote.adp_200,
                    AverageDailyPrice50 = quote.adp_50,
                    AverageDailyVolume21 = quote.adv_21,
                    AverageDailyVolume30 = quote.adv_30,
                    AverageDailyVolume90 = quote.adv_90,
                    AskPrice = quote.ask,
                    AskSize = quote.asksz,
                    BidPrice = quote.bid,
                    BidSize = quote.bidsz,
                    BetaVolatility = quote.beta,
                    PreviousCandleClose = quote.cl,
                    Dividend = quote.div,
                    DividendExDate = Utils.ParseYYYYMMDDDate(quote.divexdate.ToString()),
                    EarningsPerShare = quote.eps,
                    High = quote.hi,
                    LastTradeVolume = quote.incr_vl,
                    Last = quote.last,
                    Low = quote.lo,
                    Name = quote.name,
                    Open = quote.opn,
                    PriorDayClose = quote.pcls,
                    PriorDayHigh = quote.phi,
                    PriorDayLow = quote.plo,
                    PriorDayOpen = quote.popn,
                    PriorDayVolume = quote.pvol,
                    PriceEarnings = quote.pe,
                    SharesOutstanding = quote.sho,
                    Symbol = quote.symbol,
                    TradesSinceMarketOpen = quote.tr_num,
                    TrendTenTicks = quote.trend,
                    Volume = quote.vl,
                    VolatilityOneYear = quote.volatility12,
                    VolumeWeightedAveragePrice = quote.vwap,
                    High52Week = quote.wk52hi,
                    High52WeekDate = Utils.ParseYYYYMMDDDate(quote.wk52hidate.ToString()),
                    Low52Week = quote.wk52lo,
                    Low52WeekDate = Utils.ParseYYYYMMDDDate(quote.wk52lodate.ToString()),
                    DividendYieldAsPercent = quote.yield
                };

                return marketQuoteModel;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error in MarketQuoteModel.ParseQuote: {ex.ToString()}");
            }
            

            return new MarketQuoteModel();
        }

    }
        
}
