using EcommerceDomainDrivenDesign.Domain.Core.Base;
using EcommerceDomainDrivenDesign.Domain.Core.Messaging;
using EcommerceDomainDrivenDesign.Domain.Customers;
using EcommerceDomainDrivenDesign.Domain.Products;

namespace EcommerceDomainDrivenDesign.Domain
{
    public interface IEcommerceUnitOfWork : IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }
        IStoredEventRepository MessageRepository { get; }
        IProductRepository ProductRepository { get; }
    }
}
