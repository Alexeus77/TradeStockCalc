using System.Reflection;
using System.IO;

namespace UnitTestTradeStockCalc.Helpers
{
    static class Helper
    {
        public const string xmlResource = "UnitTestTradeStockCalc.Resources.Trades.xml";
        public const string csvResource = "UnitTestTradeStockCalc.Resources.Trades.csv";
        public const string csvFXOptionsResource = "UnitTestTradeStockCalc.Resources.FXOptions.csv";

        public static Stream LoadTestResource(string resourceName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        }
    }
}
