using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ci.trading.service.api
{
    public interface IAccountService
    {
        Task<string> GetAccountInfo();
    }
}
