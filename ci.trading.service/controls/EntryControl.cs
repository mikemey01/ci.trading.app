using ci.trading.models.app;
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
        private readonly IMarketTimeSales _timeSalesService;
        private readonly ILogger<EntryControl> _logger;
        private readonly AppSettings _config;

        public EntryControl(
            IAccountService accountService,
            IMarketClockService clockService,
            IMarketQuoteService quoteService,
            IMarketTimeSales timeSalesService,
            ILogger<EntryControl> logger,
            IOptions<AppSettings> config
            )
        {
            _accountService = accountService;
            _clockService = clockService;
            _quoteService = quoteService;
            _timeSalesService = timeSalesService;
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

            var date = new DateTime(2020, 1, 23);
            var timeSales = await _timeSalesService.CallApi(httpClient, "1min", "F", date);
        }
    }
}
