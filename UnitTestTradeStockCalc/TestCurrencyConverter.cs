using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TradeStockCalc;
using TradeStockCalc.WebUtils;
using TradeStockCalc.Converter;
using TradeStockCalc.Data;

namespace UnitTestTradeStockCalc
{
    [TestClass]
    public class TestCurrencyConverter
    {
        [TestMethod]
        public void ConvertWithECB_UseStubWebClientResponse_ReturnsFixedValue()
        {
            string response =
                "{\"base\":\"USD\",\"date\":\"2017 - 07 - 10\",\"rates\":{\"PLN\":3.7183}}";

            ECBCurrencyConverter cc = new ECBCurrencyConverter();
            
            cc.Webclient = Substitute.For<IWebClientRequest>();
            cc.Webclient.MakeRequest(Arg.Any<string>()).Returns(response);

            var val = cc.Convert(Currency.USD, Currency.PLN, 100);

            Assert.AreEqual(val, 371.8300M);
        }
    }
}
