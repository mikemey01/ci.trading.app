using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ci.trading.service.api
{
    public class AccountService : IAccountService
    {
        public AccountService()
        {

        }

        public async Task<string> GetAccountInfo()
        {
            return "Account info";
        }
    }
}
