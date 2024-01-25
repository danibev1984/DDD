using Marketplace.Framework;
using System;

namespace Marketplace.Domain
{
    public class Money : Value<Money>
    {
        public static Money FromDecimal(decimal amount, string currency = DefaultCurrency) => new Money(amount, currency);
        public static Money FromString(string amount, string currency = DefaultCurrency) => new Money(decimal.Parse(amount), currency);

        private const string DefaultCurrency = "EUR";

        public decimal Amount { get; }
        public string Currency { get; }

        protected Money(decimal amount, string currency = DefaultCurrency)
        {
            if (decimal.Round(amount, 2) != amount)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot have more then two decimals");

            Amount = amount;
            Currency = currency;
        }

        public Money Add(Money summand)
        {
            if (Currency != summand.Currency)
                throw new CurrencyMismatchException("cannot sum amounts with different currencies");

            return new Money(Amount + summand.Amount, Currency);
        }

        public Money Substract(Money subtrahend)
        {
            if (Currency != subtrahend.Currency)
                throw new CurrencyMismatchException("cannot substract amounts with different currencies");

            return new Money(Amount - subtrahend.Amount, Currency);
        }
        public static Money operator +(Money summand1, Money summand2) => summand1.Add(summand2);
        public static Money operator -(Money minuend, Money subtrahend) => minuend.Add(subtrahend);
    }
}
