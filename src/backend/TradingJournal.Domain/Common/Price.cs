using TradingJournal.Domain.Errors;
using TradingJournal.Domain.Infrastructure;
using TradingJournal.SharedKernel;

namespace TradingJournal.Domain.Common
{
    /// <summary>
    /// Цена
    /// </summary>
    public class Price : ValueObject
    {
        /// <summary>
        /// Значение
        /// </summary>
        public decimal Value { get; }

        public Price(decimal value)
        {
            if (value < 0)
                throw new DomainException<Price>(PriceErrors.PriceCanNotBeNegative);
        }

        protected Price() { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator decimal(Price price) => price.Value;
        public static explicit operator Price(decimal value) => new Price(value);

        public override string ToString() => Value.ToString("F8");
    }
}
