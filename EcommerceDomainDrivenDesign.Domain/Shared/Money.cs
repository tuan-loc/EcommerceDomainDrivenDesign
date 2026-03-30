using EcommerceDomainDrivenDesign.Domain.Core.Base;
using System.Collections.Generic;

namespace EcommerceDomainDrivenDesign.Domain.Shared
{
    public class Money : ValueObject
    {
        public decimal Value { get; }
        public Currency Currency { get; }

        private Money(decimal value, string currency)
        {
            Value = value;
            Currency = Currency.FromCode(currency);
        }

        public static Money Of(decimal value, string currency)
        {
            if (string.IsNullOrEmpty(currency))
                throw new BusinessRuleException("Money must have currency.");

            return new Money(value, currency);
        }

        public static Money operator *(decimal number, Money rightValue)
        {
            return new Money(number * rightValue.Value, rightValue.Currency.Name);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return Value;
            yield return Currency;
        }

        private Money() { }
    }
}
