using Marketplace.Domain;

namespace Marketplace.tests
{
    public class FakeCurrencyLookup : ICurrencyLookup
    {
        private static readonly IEnumerable<CurrencyDetails> _currencies = new[] {
            new CurrencyDetails { CurrencyCode = "EUR", DecimalPlaces = 2, InUse = true },
            new CurrencyDetails { CurrencyCode = "USD", DecimalPlaces = 2, InUse = true },
            new CurrencyDetails { CurrencyCode = "DEM", DecimalPlaces = 2, InUse = false },
        };

        public CurrencyDetails FindCurrency(string currencyCode) => _currencies.FirstOrDefault(x => x.CurrencyCode == currencyCode) ?? CurrencyDetails.None;
    }

    public class MoneyTests
    {
        private static readonly ICurrencyLookup _currencyLookup = new FakeCurrencyLookup();

        [Fact]
        public void Money_objects_with_the_same_amount_should_be_equal()
        {
            var firstAmount = Money.FromDecimal(5, "EUR", _currencyLookup);
            var secondAmount = Money.FromDecimal(5, "EUR", _currencyLookup);

            Assert.Equal(firstAmount, secondAmount);
        }

        [Fact]
        public void Money_objects_with_the_same_amount_but_different_currencies_should_not_be_equal()
        {
            var firstAmount = Money.FromDecimal(5, "EUR", _currencyLookup);
            var secondAmount = Money.FromDecimal(5, "USD", _currencyLookup);

            Assert.NotEqual(firstAmount, secondAmount);
        }

        [Fact]
        public void FromString_and_FromDecimal_should_be_equal()
        {
            var firstAmount = Money.FromDecimal(5, "EUR", _currencyLookup);
            var secondAmount = Money.FromString("5,00", "EUR", _currencyLookup);
            Assert.Equal(firstAmount, secondAmount);
        }
       
        [Fact]
        public void Sum_of_money_gives_full_amount()
        {
            var coin1 = Money.FromDecimal(1, "EUR", _currencyLookup);
            var coin2 = Money.FromDecimal(2, "EUR", _currencyLookup);
            var coin3 = Money.FromDecimal(2, "EUR", _currencyLookup);
            var banknote = Money.FromDecimal(5, "EUR", _currencyLookup);

            Assert.Equal(banknote, coin1 + coin2 + coin3);
        }

        [Fact]
        public void Unused_currency_should_not_be_allowed() => Assert.Throws<ArgumentException>(() => Money.FromDecimal(100, "DEM", _currencyLookup));

        [Fact]
        public void Unknown_currency_should_not_be_allowed() => Assert.Throws<ArgumentException>(() => Money.FromDecimal(100, "WHAT?", _currencyLookup));
        

        [Fact]
        public void Throw_when_too_many_decimal_places() => Assert.Throws<ArgumentOutOfRangeException>(() => Money.FromDecimal(100.123m, "EUR", _currencyLookup));

        [Fact]
        public void Throws_on_adding_different_currencies()
        {
            var firstAmount = Money.FromDecimal(5, "USD", _currencyLookup);
            var secondAmount = Money.FromDecimal(5, "EUR", _currencyLookup);
            
            Assert.Throws<CurrencyMismatchException>(() => firstAmount + secondAmount);
        }

        [Fact]
        public void Throws_on_substracting_different_currencies()
        {
            var firstAmount = Money.FromDecimal(5, "USD", _currencyLookup);
            var secondAmount = Money.FromDecimal(5, "EUR", _currencyLookup);

            Assert.Throws<CurrencyMismatchException>(() => firstAmount - secondAmount);
        }
    }
}
