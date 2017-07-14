using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using TradeStockCalc.Data;

namespace TradeStockCalc.Converter
{
    /// <summary>
    /// Gets currecny convertion rates from ECB service
    /// </summary>
    public class ECBCurrencyConverter : CurrencyConverterBase, ICurrencyConvertor
    {
        JavaScriptSerializer _jserializer = new JavaScriptSerializer();
        ECBResponse _responseDeserialized = null;

        private static string Url { get; } = "http://api.fixer.io/latest?base={0}";
        public static string ServiceUrl { get; } = "http://api.fixer.io/latest";


        protected override decimal GetRateFromWebService(Currency inputCurrency, Currency outputCurrency)
        {
            string request = string.Format(Url, inputCurrency);

            string response = Webclient.MakeRequest(request);

            _responseDeserialized = _jserializer.Deserialize<ECBResponse>(response);

            var rate = _responseDeserialized.rates.Where(r => r.Key == outputCurrency.ToString()).
                FirstOrDefault();

            return rate.Value;
        }

        class ECBResponse
        {
            public string @base;
            public DateTime date;
            public Dictionary<string, decimal> rates = new Dictionary<string, decimal>();
        }
    }
}
