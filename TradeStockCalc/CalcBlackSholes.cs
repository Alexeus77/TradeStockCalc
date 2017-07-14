using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace TradeStockCalc
{
    /// <summary>
    /// makes calculations on BlackSchole algorithm
    /// </summary>
    public static class CalcBlackSholes
    {
        
        private static double CalcD1(double stockPrice, double strikePrice, double timeToMatury, double riskFreeRate, double volatily)
        {
            return (Math.Log(stockPrice / strikePrice) + (riskFreeRate + volatily * volatily / 2.0) * timeToMatury) / (volatily * Math.Sqrt(timeToMatury));
        }

        private static double CalcD2(double stockPrice, double strikePrice, double timeToMatury, double riskFreeRate, double volatily)
        {
            double d1 = CalcD1(stockPrice, strikePrice, timeToMatury, riskFreeRate, volatily);
            return CalcD2(d1, timeToMatury, volatily);
        }

        private static double CalcD2(double d1, double timeToMatury, double volatily)
        {
            return d1 - volatily * Math.Sqrt(timeToMatury);
        }
        
        public static double BlackScholesCall(double stockPrice, double strikePrice,
            double timeToMatury, double riskFreeRate, double volatily)
        {

            riskFreeRate /= 100;
            volatily /= 100;

            double d1 = CalcD1(stockPrice, strikePrice, timeToMatury, riskFreeRate, volatily);
            double d2 = CalcD2(d1, timeToMatury, volatily);
            
            double dBlackScholes = CND(d1) *stockPrice  - CND(d2) *strikePrice * Math.Exp(-riskFreeRate * timeToMatury);
            
            return dBlackScholes;
        }

        public static double BlackScholesPut(double stockPrice, double strikePrice,
           double timeToMatury, double riskFreeRate, double volatily)
        {
            riskFreeRate /= 100;
            volatily /= 100;

            double d1 = CalcD1(stockPrice, strikePrice, timeToMatury, riskFreeRate, volatily);
            double d2 = CalcD2(d1, timeToMatury, volatily);

            double dBlackScholes = strikePrice * Math.Exp(-riskFreeRate * timeToMatury) * CND(-d2) - stockPrice * CND(-d1);

            return dBlackScholes;
        }

        /// <summary>
        /// Calculates normal distribution for given value using System.Windows.Forms.DataVisualization.StatisticFormula.NormalDistribution function
        /// </summary>
        /// <param name="X"></param>
        /// <returns></returns>
        private static double CND(double X)
        {
            Chart chart = new Chart();
            return chart.DataManipulator.Statistics.NormalDistribution(X);
        }
        
    }
}
