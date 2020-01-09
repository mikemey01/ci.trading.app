using ci.trading.models.app;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ci.trading.service.api
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly AppSettings _config;
        private const string ACCOUNT_URL = "https://api.tradeking.com/v1/accounts.json";
        public AccountService(
            ILogger<AccountService> logger,
            IOptions<AppSettings> config)
        {
            _logger = logger;
            _config = config.Value;
        }

        public async Task<string> GetAccountInfo()
        {
            _logger.LogInformation($"Paper endpoint: {_config.ApiEndpoint}");
            _logger.LogInformation($"Paper key: {_config.ConsumerKey}");

            using (var httpClient = new HttpClient())
            {
                Utils.SetupApiCall(_config, ACCOUNT_URL, "GET", httpClient);
                
                try
                {
                    var response = await httpClient.GetAsync(ACCOUNT_URL);
                    var data = response.Content.ReadAsStringAsync();
                    // deserialize
                }
                catch(Exception ex)
                {
                    _logger.LogError($"Error in AccountService.GetAccountInfo: {ex}");
                }
                
                var test = "";
            }

                return "Account info";
        }
    }
}
