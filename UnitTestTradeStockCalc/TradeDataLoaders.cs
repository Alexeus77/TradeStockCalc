using System;
using System.IO;
using System.Linq;
using TradeStockCalc.DataLoaders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestTradeStockCalc.Helpers;

namespace UnitTestTradeStockCalc
{
    /// <summary>
    /// Summary description for TestLoaders
    /// </summary>
    [TestClass]
    public class TradeDataLoaders
    {

        [TestMethod]
        [ExpectedException(typeof(System.FormatException),
                "Exception of invalid format should be thrown.")]

        public void CompositeLoader_LoadResourceOfUnexpectedFormat_ExpectError()
        {
            Load(Helper.csvFXOptionsResource, r => new TradeDataLoaderComposite(r, DataParser.StockOptionsTradesParser));
        }

        [TestMethod]
        [ExpectedException(typeof(System.FormatException),
                "Exception of invalid format should be thrown.")]
        public void CSVLoader_LoadXMLResource_ExpectError()
        {
            
            Load(Helper.xmlResource, r => new CSVLoader(r, DataParser.StockOptionsTradesParser));
        }

        [TestMethod]
        [ExpectedException(typeof(System.FormatException),
                "Exception of invalid format should be thrown.")]
        public void XMLLoader_LoadCSVResource_ExpectError()
        {
            Load(Helper.csvResource, r => new XMLLoader(r, DataParser.StockOptionsTradesParser));
        }

        [TestMethod]
        public void CSVLoader_LoadValidResource_RecordsMoreThanZero()
        {
            Load(Helper.csvResource, r => new CSVLoader(r, DataParser.StockOptionsTradesParser));
        }

        [TestMethod]
        public void XMLLoader_LoadValidResource_RecordsMoreThanZero()
        {
            Load(Helper.xmlResource, r => new XMLLoader(r, DataParser.StockOptionsTradesParser));
        }

        [TestMethod]
        public void CompositeLoader_LoadBothValidResources_RecordsMoreThanZero()
        {
            Load(Helper.xmlResource, r => new TradeDataLoaderComposite(r, DataParser.StockOptionsTradesParser));
            Load(Helper.csvResource, r => new TradeDataLoaderComposite(r, DataParser.StockOptionsTradesParser));
        }

        private void Load(string resourceName, Func<Stream, ITradeDataLoader> newFunc)
        {
            Stream resStream = Helpers.Helper.LoadTestResource(resourceName);
            ITradeDataLoader dataloader = newFunc(resStream);

            Assert.IsTrue(dataloader.GetTradeData().Count() > 0);
        }

        
    }
}
