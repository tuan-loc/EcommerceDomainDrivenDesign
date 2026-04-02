namespace EcommerceDomainDrivenDesign.Domain.Services
{
    public interface ICustomerUniquenessChecker
    {
        bool IsUserUnique(string customerEmail);
    }
}
