using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TradeStockCalc;
using TradeStockCalc.Data;
using TradeStockCalc.DataLoaders;
using TradeStockCalc.Converter;
using UnitTestTradeStockCalc.Helpers;


namespace UnitTestTradeStockCalc
{
    [TestClass]
    public class TestCalculation
    {
        [TestMethod]
        public void MakeCalculation()
        {
            var inputStream = Helper.LoadTestResource(Helper.xmlResource);
            var loader = new TradeDataLoaderComposite(inputStream, DataParser.StockOptionsTradesParser);

            IDictionary<string, StockData> stockData = new
                Dictionary<string, StockData>(2, StringComparer.OrdinalIgnoreCase);

            stockData.Add("ABC INC",
                new StockData
                {
                    SpotPrice = new Price { currency = Currency.PLN, Value = 30 },
                    Volatily = 40
                });

            stockData.Add("CDE LTD",
                new StockData
                {
                    SpotPrice = new Price { currency = Currency.USD , Value = 110 },
                    Volatily = 5
                });

            IDictionary<Currency, double> riskRate = new
                Dictionary<Currency, double>(2);

            riskRate.Add(Currency.PLN, 5);
            riskRate.Add(Currency.USD, 3);

            Price.convertor = new ECBCurrencyConverter();
            
            foreach (var data in loader.GetTradeData())
                
            {
                var result = Calc.CalculateBS(data, stockData, riskRate,
                    DateTime.Parse(" 01.04.2016"), Currency.USD);

                TestContext.WriteLine(
                    "Name: {0}, Spot: {1}, Strike: {2}, Volatily: {3}, Risk: {4}, Exp: {5}, Result: {6}, Result type: {7}",
                    data.Name, 
                    stockData[data.Name].SpotPrice.Convert(Currency.USD).Value,
                    data.StrikePrice.Convert(Currency.USD).Value,
                    stockData[data.Name].Volatily,
                    riskRate[Currency.USD],
                    data.Expiry,
                    result,
                    data.cp == CP.C ? "CALL" : "PUT");
            }
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
    }
}
