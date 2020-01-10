using ci.trading.models.app;
using ci.trading.service.api;
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
        private readonly ILogger<EntryControl> _logger;
        private readonly AppSettings _config;
        public EntryControl(
            IAccountService accountService,
            ILogger<EntryControl> logger,
            IOptions<AppSettings> config
            )
        {
            _accountService = accountService;
            _logger = logger;
            _config = config.Value;
        }

        public async Task StartResearch()
        {
            var httpClient = new HttpClient();
            var accountInfo = await _accountService.CallApi(httpClient);
            _logger.LogInformation($"a test information: {_config.TokenKey}");
        }
    }
}
