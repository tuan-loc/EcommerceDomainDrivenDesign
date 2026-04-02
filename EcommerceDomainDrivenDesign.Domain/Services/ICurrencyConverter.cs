using EcommerceDomainDrivenDesign.Domain.Shared;

namespace EcommerceDomainDrivenDesign.Domain.Services
{
    public interface ICurrencyConverter
    {
        Currency GetBaseCurrency();
        Money Convert(Currency currency, Money value);
    }
}