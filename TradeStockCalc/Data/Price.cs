using System;
using TradeStockCalc.Converter;

namespace TradeStockCalc.Data
{
    public struct Price
    {
        public static ICurrencyConvertor convertor = new Price.DefaultConvertor();

        public MyDecimal Value;
        public Currency currency;

        public struct MyDecimal
        {
            decimal intValue;

            public static implicit operator double(MyDecimal value)
            {
                return System.Convert.ToDouble(value.intValue);
            }

            public static implicit operator string(MyDecimal value)
            {
                return value.intValue.ToString("N2");
            }

            public static implicit operator MyDecimal(string value)
            {
                return new MyDecimal { intValue = decimal.Parse(value) };
            }

            public static implicit operator decimal(MyDecimal value)
            {
                return value.intValue;
            }

            public static implicit operator MyDecimal(int value)
            {
                return new MyDecimal { intValue = value };
            }

            public static implicit operator MyDecimal(double value)
            {
                return new MyDecimal { intValue = (decimal)value };
            }

            public static implicit operator MyDecimal(decimal value)
            {
                return new MyDecimal { intValue = value };
            }

            public static MyDecimal operator +(MyDecimal value1, MyDecimal value2)
            {
                return new MyDecimal { intValue = value1.intValue + value2.intValue };
            }

            public override string ToString()
            {
                return intValue.ToString("N2");
            }
        }
        
        public Price Convert(Currency toCurrency)
        {
            decimal newValue = convertor.Convert(currency, toCurrency, Value);

            return ConverterHelper.Convert(this, toCurrency, convertor);
        }

        public static Price Default
        {
            get
            {
                return new Price { Value = 0, currency = Currency.USD };
            }
        }

        class DefaultConvertor : ICurrencyConvertor
        {
            public decimal Convert(Currency inputCurrency, Currency outputCurrency, decimal value)
            {
                return value;
            }

            public decimal GetRate(Currency inputCurrency, Currency outputCurrency)
            {
                return 1;
            }
        }
    }
}
