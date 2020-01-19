using ci.trading.models.account;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ci.trading.service.api
{
    public interface IAccountService
    {
        Task<AccountModel> CallApi(HttpClient httpClient);
    }
}
