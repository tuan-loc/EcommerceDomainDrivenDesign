namespace EcommerceDomainDrivenDesign.Domain.Customers
{
    public interface ICustomerUniquenessChecker
    {
        bool IsUserUnique(string customerEmail);
    }
}
