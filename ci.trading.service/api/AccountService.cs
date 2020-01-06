using ci.trading.models.app;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ci.trading.service.api
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly AppSettings _config;
        public AccountService(
            ILogger<AccountService> logger,
            IOptions<AppSettings> config)
        {
            _logger = logger;
            _config = config.Value;
        }

        public async Task<string> GetAccountInfo()
        {
            _logger.LogInformation($"Paper endpoint: {_config.ApiPaperEndpoint}");
            _logger.LogInformation($"Paper key: {_config.ApiPaperKey}");
            return "Account info";
        }
    }
}
