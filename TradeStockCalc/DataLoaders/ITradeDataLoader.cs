using System.Collections.Generic;
using TradeStockCalc.Data;

namespace TradeStockCalc.DataLoaders
{
    public interface ITradeDataLoader
    {
        IEnumerable<TradeData> GetTradeData();
    }
}
