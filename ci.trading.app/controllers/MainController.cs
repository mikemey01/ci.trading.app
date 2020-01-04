using ci.trading.service.api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ci.trading.app.controllers
{
    public class MainController : IMainController
    {
        private readonly IAccountService _accountService;
        public MainController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task StartTrading()
        {
            var accountInfo = await _accountService.GetAccountInfo();
        }
    }
}
