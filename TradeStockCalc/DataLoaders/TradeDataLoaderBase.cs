using System;
using System.IO;
using TradeStockCalc.Data;

namespace TradeStockCalc.DataLoaders
{
    public class TradeDataLoaderBase
    {
        protected Stream _fileStream;
        protected Func<string[], TradeData> _fieldsParser;
        
        public TradeDataLoaderBase(Stream fileStream, 
            Func<string[], TradeData> fieldsParser)
        {
            _fileStream = fileStream;
            _fieldsParser = fieldsParser;
        }

        protected TradeData Parse(params string[] fields)
        {
            return _fieldsParser(fields);
        }
        
    }
}
