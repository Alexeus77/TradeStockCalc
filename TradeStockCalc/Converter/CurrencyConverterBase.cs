using System;
using TradeStockCalc.WebUtils;
using TradeStockCalc.Data;
using System.Collections.Generic;

namespace TradeStockCalc.Converter
{
    public abstract class CurrencyConverterBase
    {
        protected IDictionary<Tuple<Currency, Currency>, decimal> CachedRates 
            = new Dictionary<Tuple<Currency, Currency>, decimal>();

        public IWebClientRequest Webclient { get; set; } = new WebClientWrapper();
        abstract protected decimal GetRateFromWebService(Currency inputCurrency, Currency outputCurrency);
        public decimal GetRate(Currency inputCurrency, Currency outputCurrency)
        {
            var rateKey = new Tuple<Currency, Currency>(inputCurrency, outputCurrency);

            if (CachedRates.ContainsKey(rateKey))
                return CachedRates[rateKey];

            //TODO: need to fill all cache rates for given currency at once
            var rate = GetRateFromWebService(inputCurrency, outputCurrency);
            CachedRates.Add(rateKey, rate);

            return rate;
        }

        public decimal Convert(Currency inputCurrency, Currency outputCurrency, decimal value)
        {
            if (inputCurrency == outputCurrency)
                return value;

            decimal rate = GetRate(inputCurrency, outputCurrency);
            return rate * value;
        }
    }
}
