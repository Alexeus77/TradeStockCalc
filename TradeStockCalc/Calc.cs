using System;
using System.Collections.Generic;
using System.Linq;
using TradeStockCalc.Data;
using TradeStockCalc.Helpers;

namespace TradeStockCalc
{
    /// <summary>
    /// Performs trade calculations
    /// </summary>
    public static class Calc
    {
        static Func<double, double, double, double, double, double> functionCall = CalcBlackSholes.BlackScholesCall;
        static Func<double, double, double, double, double, double> functionPut = CalcBlackSholes.BlackScholesPut;

        public static double CalculateBS(TradeData trade,
            IDictionary<string, StockData> stockData,
            IDictionary<Currency, double> currencyRiskRates,
            DateTime currentTime, Currency currency)
        {
            if (!stockData.ContainsKey(trade.Name))
                throw new ArgumentException("No stock data for given trade.");

            if (!currencyRiskRates.ContainsKey(currency))
                throw new ArgumentException("No risk data for given currency.");
            
            return  CalculateBS(
                    trade,
                    stockData[trade.Name].SpotPrice.Convert(currency).Value,
                    stockData[trade.Name].Volatily,
                    currencyRiskRates,
                    currentTime, currency);
        }

        public static double CalculateBS(TradeData trade,
            double spotPrice,
            double volatily,
            IDictionary<Currency, double> currencyRiskRates,
            DateTime currentTime, Currency currency)
        {
            
            if (!currencyRiskRates.ContainsKey(currency))
                throw new ArgumentException("No risk data for given currency.");

            return Calculate(
                    trade.cp == CP.C ?
                    functionCall : functionPut,
                    spotPrice,
                    trade.StrikePrice.Value,
                    currentTime.GetYearsDiff(trade.Expiry),
                    currencyRiskRates[currency],
                    volatily
                    );
        }

        //public static IEnumerable<double> Calculate(IEnumerable<TradeData> tradeData,
        //    IDictionary<string, StockData> stockData, 
        //    IDictionary<Currency, double> currencyRiskRates,
        //    DateTime currentTime, Currency targetCurrency)
        //{
        //    return tradeData.
        //        Where(t => stockData.ContainsKey(t.Name)).
        //        Where(t => currencyRiskRates.ContainsKey(targetCurrency)).
        //        Select(trade =>
        //        Calculate(
        //            trade.cp == CP.C ?
        //            functionCall : functionPut,
        //            stockData[trade.Name].SpotPrice.Convert(targetCurrency).Value.ToDouble(),
        //            trade.StrikePrice.Convert(targetCurrency).Value.ToDouble(),
        //            currentTime.GetYearsDiff(trade.Expiry),
        //            currencyRiskRates[targetCurrency],
        //            stockData[trade.Name].Volatily
        //            ));
        //}

        //private static double Calculate

        private static double Calculate(Func<double, double, double, double, double, double> calculateFunction,
            double stockPrice, double strikePrice,
            double timeToMatury, double riskFreeRate, double volatily)
        {
            return calculateFunction(stockPrice, strikePrice, timeToMatury, riskFreeRate, volatily);
        }
    }
}
