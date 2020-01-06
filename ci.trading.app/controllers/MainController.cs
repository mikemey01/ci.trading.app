using ci.trading.models.app;
using ci.trading.service.api;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ci.trading.app.controllers
{
    public class MainController : IMainController
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<MainController> _logger;
        private readonly AppSettings _config;
        public MainController(
            IAccountService accountService, 
            ILogger<MainController> logger,
            IOptions<AppSettings> config)
        {
            _accountService = accountService;
            _logger = logger;
            _config = config.Value;
        }

        public async Task StartTrading()
        {
            var accountInfo = await _accountService.GetAccountInfo();
            _logger.LogInformation($"a test information: {_config.ApiLive}");
        }
    }
}
