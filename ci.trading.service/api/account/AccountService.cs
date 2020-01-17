using ci.trading.models.account;
using ci.trading.models.app;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private readonly AppSettings _appSettings;
        private const string ACCOUNT_URL = "https://api.tradeking.com/v1/accounts.json";

        public AccountService(
            ILogger<AccountService> logger,
            IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        public async Task<AccountModel> CallApi(HttpClient httpClient)
        {
            Utils.SetupApiCall(_appSettings, ACCOUNT_URL, "GET", httpClient);
            var accountModel = new AccountModel();

            try
            {
                var response = await httpClient.GetAsync(ACCOUNT_URL);
                var data = await response.Content.ReadAsStringAsync();
                accountModel = await ParseResponse(data);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error in AccountService.CallApi: {ex}");
            }

            return accountModel;
        }

        public async Task<AccountModel> ParseResponse(string data)
        {
            var accountModel = new AccountModel();
            try
            {
                dynamic dynamicResponse = JsonConvert.DeserializeObject(data);
                var response = dynamicResponse.response;
                if(response.error == "Success")
                {
                    accountModel.ResponseId = response["@id"] ?? "";
                    accountModel.AccountId = response.accounts?.accountsummary?.account ?? "";
                    accountModel.AccountValue = response.accounts?.accountsummary?.accountbalance?.accountvalue ?? 0;
                    accountModel.CashAvailable = response.accounts?.accountsummary?.accountbalance?.money?.cashavailable ?? 0;
                    accountModel.UnsettledFunds = response.accounts?.accountsummary?.accountbalance?.money?.unsettledfunds ?? 0;
                    accountModel.UnclearedDeposits = response.accounts?.accountsummary?.accountbalance?.money?.uncleareddeposits ?? 0;
                    accountModel.OptionValue = response.accounts?.accountsummary?.accountbalance?.securities?.options ?? 0;
                    accountModel.StockValue = response.accounts?.accountsummary?.accountbalance?.securities?.stocks ?? 0;
                    accountModel.BuyingPower = response.accounts?.accountsummary?.accountbalance?.buyingpower?.stock ?? 0;
                }
                else
                {
                    accountModel.IsSuccessful = false;
                    accountModel.Error = response.error;
                }
                
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error in AccountService.ParseResponse: {ex.ToString()}");
            }

            return accountModel;
            
        }
    }
}
