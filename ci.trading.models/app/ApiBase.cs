using System;
using System.Collections.Generic;
using System.Text;

namespace ci.trading.models.app
{
    public class ApiBase
    {
        public bool IsSuccessful { get; set; } = true;
        public string Error { get; set; }
        public string ResponseId { get; set; }
    }
}
