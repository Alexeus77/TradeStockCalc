using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradeStockCalc.Data;
using System.Xml.Linq;

namespace TradeStockCalc.DataLoaders
{
    /// <summary>
    /// Parses input data
    /// </summary>
    public static class DataParser
    {
        static readonly string[] fieldNamesXmlStockOptionsTrades = {
            "id", "name","type", "style", "cp", "expiry", "strike", "ccy" };

        public static TradeData StockOptionsTradesParser(params string[] fields)
        {
            try
            {

                return new TradeData
                {
                    Id = Int32.Parse(fields[0]),
                    Name = fields[1],
                    type = (DataType)Enum.Parse(typeof(DataType), fields[2]),
                    style = (Style)Enum.Parse(typeof(Style), fields[3]),
                    cp = (CP)Enum.Parse(typeof(CP), fields[4]),
                    Expiry = DateTime.Parse(fields[5]),
                    StrikePrice = new Price
                    {
                        Value = fields[6],
                        currency = (Currency)Enum.Parse(typeof(Currency), fields[7])
                    }
                };
            }
            catch(ArgumentException e)
            {
                throw new FormatException(e.Message, e);
            }
        }

        public static IEnumerable<string> GetXMLAttributeStockOptionsTradesFields(XElement tradeData)
        {
            foreach (string fieldName in fieldNamesXmlStockOptionsTrades)
                yield return tradeData.Attribute(fieldName).Value;
        }
    }
}
