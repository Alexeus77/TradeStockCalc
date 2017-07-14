using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using TradeStockCalc.Data;

namespace TradeStockCalc.DataLoaders
{
    class CSVLoader : TradeDataLoaderBase, ITradeDataLoader
    {
        public CSVLoader(Stream fileStream,
            Func<string[], TradeData> fieldsParser) : base(fileStream, fieldsParser) { }

        public IEnumerable<TradeData> GetTradeData()
        {
            TextFieldParser csvParser = new TextFieldParser(_fileStream);

            csvParser.SetDelimiters(new string[] { "," });

            csvParser.ReadLine();

            while (!csvParser.EndOfData)
            {
                string[] fields = csvParser.ReadFields();

                yield return Parse(fields);
            }
        }

    }
}




