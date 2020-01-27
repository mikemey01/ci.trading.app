using ci.trading.models.app;
using ci.trading.models.markettimesales;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ci.trading.service.api.market
{
    public class MarketTimeSalesService : IMarketTimeSalesService
    {
        private readonly ILogger<MarketQuoteService> _logger;
        private readonly AppSettings _appSettings;
        private string TIME_SALES_URL = "https://api.tradeking.com/v1/market/timesales.json?";

        public MarketTimeSalesService(
            ILogger<MarketQuoteService> logger,
            IOptions<AppSettings> appSettings
            )
        {
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        public async Task<List<MarketDay>> CallApi(HttpClient httpClient, string interval, string symbol, DateTime startDate)
        {
            var endpoint = $"{TIME_SALES_URL}symbols={symbol}&startdate={Utils.GetStringDateTime(startDate)}&interval={interval}";
            Utils.SetupApiCall(_appSettings, endpoint, "GET", httpClient);
            var candleList = new List<MarketDay>();

            try
            {
                var response = await httpClient.GetAsync(endpoint);
                var data = await response.Content.ReadAsStringAsync();
                candleList = ParseResponse(data, interval);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in MarketTimeSales.CallApi: {ex.ToString()}");
            }

            return candleList;
        }

        private List<MarketDay> ParseResponse(string data, string interval)
        {
            var listCandles = new List<MarketCandle>();

            try
            {
                dynamic dynamicResponse = JsonConvert.DeserializeObject(data);
                var response = dynamicResponse.response;
                if (response.error == "Success")
                {
                    var quotes = response.quotes.quote;
                    foreach (var quote in quotes.Children())
                    {
                        var marketCandle = new MarketCandle
                        {
                            ResponseId = quote.ResponseId = response["@id"] ?? "",
                            Date = quote.datetime,
                            Open = quote.opn,
                            High = quote.hi,
                            Low = quote.lo,
                            Last = quote.last,
                            Volume = quote.vl,
                            Interval = interval
                        };
                        listCandles.Add(marketCandle);
                    }
                }
                else
                {
                    var marketDayError = new MarketDay
                    {
                        IsSuccessful = false,
                        Error = response.error
                    };

                    var listMarketDayError = new List<MarketDay>();
                    listMarketDayError.Add(marketDayError);

                    return listMarketDayError;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in MarketTimeSales.ParseResponse: {ex.ToString()}");
            }
            var marketCandlesByDay = MarketCandlesByDay(listCandles);
            return marketCandlesByDay;
        }

        private List<MarketDay> MarketCandlesByDay(List<MarketCandle> listCandles)
        {
            var listMarketDays = new List<MarketDay>();
            var lastTradingTime = Utils.GetMarketCloseTime(); // used to filter out after hours trades

            if (listCandles.Count == 0)
                return listMarketDays;

            // get all the days we need to process
            var listDistinctDates = listCandles.Select(x => x.Date.Date).Distinct().ToList();

            foreach (var date in listDistinctDates)
            {
                var marketDay = new MarketDay
                {
                    Date = date,
                    Candles = listCandles.FindAll(x => x.Date.Date == date && x.Date.TimeOfDay <= lastTradingTime), // filter for date/time-of-day
                    interval = listCandles.First(x => x.Date.Date == date).Interval
                };

                // add to the list
                listMarketDays.Add(marketDay);
            }

            return listMarketDays;
        }
    }
}
