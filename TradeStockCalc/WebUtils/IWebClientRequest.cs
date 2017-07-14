using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeStockCalc.WebUtils
{
    public interface IWebClientRequest : IDisposable
    {
        string MakeRequest(string request);
    }
}
