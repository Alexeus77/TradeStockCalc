using System;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using TradeStockCalc.Data;

namespace TradeStockCalc.DataLoaders
{
    class XMLLoader : TradeDataLoaderBase, ITradeDataLoader
    {
        public XMLLoader(Stream fileStream,
            Func<string[], TradeData> fieldsParser) : base(fileStream, fieldsParser) { }

        public IEnumerable<TradeData> GetTradeData()
        {
            try
            {
                XDocument doc = XDocument.Load(_fileStream);

                var result = from tradedata in doc.Descendants("portfolio").Elements()
                             select
                             Parse(
                                 DataParser.GetXMLAttributeStockOptionsTradesFields(tradedata).
                                 ToArray()
                                 );

                return result;
            }
            catch (System.Xml.XmlException e)
            {
                throw new FormatException(e.Message);
            }
        }
    }
}
