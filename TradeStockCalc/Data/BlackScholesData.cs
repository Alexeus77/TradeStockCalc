using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeStockCalc.Data
{
    public struct BlackScholesData
    {
        public double stockPrice;
        public double strikePrice;
        public double timeToMatury;
        public double riskFreeRate;
        public double volatily;
    }
}
