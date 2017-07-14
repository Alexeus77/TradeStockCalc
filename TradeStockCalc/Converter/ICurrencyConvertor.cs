using TradeStockCalc.Data;

namespace TradeStockCalc.Converter
{
    public interface ICurrencyConvertor
    {
        decimal Convert(Currency inputCurrency, Currency outputCurrency, decimal value);
        decimal GetRate(Currency inputCurrency, Currency outputCurrency);
    }
}
