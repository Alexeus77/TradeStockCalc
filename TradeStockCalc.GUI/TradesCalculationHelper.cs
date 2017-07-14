using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradeStockCalc.Data;
using TradeStockCalc.Helpers;
using TradeStockCalc.DataLoaders;
using System.IO;

namespace TradeStockCalc.GUI
{
    static class TradesCalculationHelper
    {
        private static Price CalculateTrade(TradeData trade, Currency targetCurrency,
            double spotPriceDeviation, double volatilyDeviation)
        {
            double result = Calc.CalculateBS(trade,
                        InitialData.stockData[trade.Name].SpotPrice.Value + spotPriceDeviation,
                        InitialData.stockData[trade.Name].Volatily + volatilyDeviation,
                        InitialData.riskRate,
                        InitialData.CurrentDate, trade.StrikePrice.currency);

            return
                (new Price { currency = trade.StrikePrice.currency, Value = result }).
                Convert(targetCurrency);
        }

        public static IEnumerable<Tuple<TradeData, Price, Currency>> CalculateTrades(Stream inputStream,
            Currency targetCurrency,
            Func<TradeData, bool> tradesFilter,
            double spotPriceDeviation = 0, double volatilyDeviation = 0)
        {
            ITradeDataLoader loader = new TradeDataLoaderComposite(inputStream, DataParser.StockOptionsTradesParser);
            
            foreach (var trade in loader.GetTradeData().Where(tradesFilter))

            {
                Price resultInTargetCurrency = Price.Default;
                resultInTargetCurrency = CalculateTrade(trade, targetCurrency, spotPriceDeviation, volatilyDeviation);
                
                yield return new Tuple<TradeData, Price, Currency>(trade, resultInTargetCurrency, targetCurrency);
            }

        }
    }
}
