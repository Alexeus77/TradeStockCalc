using TradeStockCalc.Data;

namespace TradeStockCalc.Converter
{
    public static class ConverterHelper
    {
        public static Price Convert(this Price price, Currency toCurrency, ICurrencyConvertor convertor)
        {
            decimal newValue = convertor.Convert(price.currency, toCurrency, price.Value);
            return new Price { Value = newValue, currency = toCurrency };
        }
        
    }
}
