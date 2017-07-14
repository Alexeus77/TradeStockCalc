using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradeStockCalc.Data;

namespace TradeStockCalc.GUI
{
    static class InitialData
    {
        public static readonly IDictionary<string, StockData> stockData = new
                Dictionary<string, StockData>(2, StringComparer.OrdinalIgnoreCase);

        public static readonly IDictionary<Currency, double> riskRate = new
                Dictionary<Currency, double>(2);

        public static readonly DateTime CurrentDate = new DateTime(2016, 4, 1);
        static InitialData()
        {

            stockData.Add("ABC INC",
                    new StockData
                    {
                        SpotPrice = new Price { currency = Currency.PLN, Value = 30 },
                        Volatily = 40
                    });

            stockData.Add("CDE LTD",
                new StockData
                {
                    SpotPrice = new Price { currency = Currency.USD, Value = 110 },
                    Volatily = 5
                });
            
            riskRate.Add(Currency.PLN, 5);
            riskRate.Add(Currency.USD, 3);
        }
    }
}
