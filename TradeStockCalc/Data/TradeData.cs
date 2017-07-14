using System;

namespace TradeStockCalc.Data
{
    public enum DataType
    {
        StockOption
    }

    public enum Style
    {
        European
    }

    public enum CP
    {
        C,
        P
    }

    public enum Currency
    {
        USD,
        PLN,
        ZAR,
        CHF
    }


    public struct TradeData
    {
        public int Id;
        public string Name;
        public DataType type;
        public Style style;
        public CP cp;
        public DateTime Expiry;
        public Price StrikePrice;
    }
}
