using ci.trading.models.app;
using ci.trading.models.markettoplist;
using ci.trading.service.api;
using ci.trading.service.api.market;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ci.trading.service.controls
{
    public class EntryControl : IEntryControl
    {
        private readonly IAccountService _accountService;
        private readonly IMarketClockService _clockService;
        private readonly IMarketQuoteService _quoteService;
        private readonly IMarketTimeSalesService _timeSalesService;
        private readonly IMarketTopListService _topListService;
        private readonly ILogger<EntryControl> _logger;
        private readonly AppSettings _config;

        public EntryControl(
            IAccountService accountService,
            IMarketClockService clockService,
            IMarketQuoteService quoteService,
            IMarketTimeSalesService timeSalesService,
            IMarketTopListService topListService,
            ILogger<EntryControl> logger,
            IOptions<AppSettings> config
            )
        {
            _accountService = accountService;
            _clockService = clockService;
            _quoteService = quoteService;
            _timeSalesService = timeSalesService;
            _topListService = topListService;
            _logger = logger;
            _config = config.Value;
        }

        public async Task StartResearch()
        {
            var httpClient = new HttpClient();
            // var accountInfo = await _accountService.CallApi(httpClient);
            //var listSymbols = new List<string>
            //{
            //    "F",
            //    "AMD",
            //    "FB"
            //};
            //var currentQuote = await _quoteService.CallApi(httpClient, listSymbols);

            //var date = new DateTime(2020, 1, 10);
            //var timeSales = await _timeSalesService.CallApi(httpClient, "5min", "F", date);

            var topList = await _topListService.CallApi(httpClient, TopListType.topgainers);
            var bottomList = await _topListService.CallApi(httpClient, TopListType.toplosers, ExchangeType.Q);
            var sb = new StringBuilder();

            foreach (var item in bottomList)
            {
                sb.AppendLine($"Name: {item.CompanyName}({item.Symbol}), Percent decrease: {item.PercentChange}, last: {item.Last}");
            }

            _logger.LogInformation(sb.ToString());
        }
    }
}
