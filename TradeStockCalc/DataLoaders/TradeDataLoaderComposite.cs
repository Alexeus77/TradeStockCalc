using System;
using System.Collections.Generic;
using TradeStockCalc.Data;
using System.IO;
using System.Linq;

namespace TradeStockCalc.DataLoaders
{
    /// <summary>
    /// Composes available loaders for input files with trades data
    /// </summary>
    public class TradeDataLoaderComposite : TradeDataLoaderBase, ITradeDataLoader
    {
        static List<Func<Stream, Func<string[], TradeData>, ITradeDataLoader>> loadersCreatorsOnStream;

        static TradeDataLoaderComposite()
        {
            loadersCreatorsOnStream = new List<Func<Stream, Func<string[], TradeData>, ITradeDataLoader>>(2);
            loadersCreatorsOnStream.Add((fileStream, fieldsParser) => new CSVLoader(fileStream, fieldsParser));
            loadersCreatorsOnStream.Add((fileStream, fieldsParser) => new XMLLoader(fileStream, fieldsParser));
        }
        public TradeDataLoaderComposite(Stream fileStream,
            Func<string[], TradeData> fieldsParser) : base(fileStream, fieldsParser) { }

        public IEnumerable<TradeData> GetTradeData()
        {
            foreach (var loaderCreator in loadersCreatorsOnStream)
            {

                _fileStream.Position = 0;

                ITradeDataLoader loader = loaderCreator(_fileStream, _fieldsParser);

                try
                {
                    return loader.GetTradeData().ToList();
                }
                catch (System.FormatException)
                {
                    continue;
                }
            }

            throw new System.FormatException("Cannot load file of specified format.");
        }

        
    }
}
