using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TradeStockCalc;
using TradeStockCalc.Data;
using TradeStockCalc.Helpers;

namespace UnitTestTradeStockCalc
{
    [TestClass]
    public class TestCalcBlackSholes
    {

        public static BlackScholesData GetTestData()
        {
            return new BlackScholesData()
            {
                stockPrice = 60,
                strikePrice = 65,
                volatily = 30,
                riskFreeRate = 8,
                timeToMatury = 0.25
            };
        }

        [TestMethod]
        public void CalcBlackSholesCall_FixedData_ReturnsPredictedValue()
        {
            var data = GetTestData();
            double result = CalcBlackSholes.BlackScholesCall(
                data.stockPrice, data.strikePrice, data.timeToMatury, data.riskFreeRate, data.volatily);

            Assert.AreEqual(result, 2.1333718619310318);
        }

        [TestMethod]
        public void CalcBlackSholesPut_FixedData_ReturnsPredictedValue()
        {
            var data = GetTestData();
            double result = CalcBlackSholes.BlackScholesPut(
                data.stockPrice, data.strikePrice, data.timeToMatury, data.riskFreeRate, data.volatily);

            Assert.AreEqual(result, 5.8462856268701273);
        }
    }
}
