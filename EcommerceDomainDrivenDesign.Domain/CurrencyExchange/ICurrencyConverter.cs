using EcommerceDomainDrivenDesign.Domain.Shared;

namespace EcommerceDomainDrivenDesign.Domain.CurrencyExchange
{
    public interface ICurrencyConverter
    {
        Currency GetBaseCurrency();
        Money Convert(string fromCurrency, Money value);
    }
}
