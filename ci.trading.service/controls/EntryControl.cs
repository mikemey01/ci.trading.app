﻿using ci.trading.models.app;
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
        private readonly ILogger<EntryControl> _logger;
        private readonly AppSettings _config;

        public EntryControl(
            IAccountService accountService,
            IMarketClockService clockService,
            IMarketQuoteService quoteService,
            ILogger<EntryControl> logger,
            IOptions<AppSettings> config
            )
        {
            _accountService = accountService;
            _clockService = clockService;
            _quoteService = quoteService;
            _logger = logger;
            _config = config.Value;
        }

        public async Task StartResearch()
        {
            var httpClient = new HttpClient();
            // var accountInfo = await _accountService.CallApi(httpClient);
            var listSymbols = new List<string>
            {
                "F"
            };
            var currentQuote = await _quoteService.CallApi(httpClient, listSymbols);
            _logger.LogInformation($"a test information: {_config.TokenKey}");
        }
    }
}