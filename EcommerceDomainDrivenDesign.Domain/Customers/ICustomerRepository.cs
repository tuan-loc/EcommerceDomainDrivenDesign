using System;
using System.Threading;
using System.Threading.Tasks;
using EcommerceDomainDrivenDesign.Domain.Core.Base;

namespace EcommerceDomainDrivenDesign.Domain.Customers
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task Add(Customer customer, CancellationToken cancellationToken = default);
        Task<Customer> GetById(Guid id, CancellationToken cancellationToken = default);
        Task<Customer> GetByEmail(string email, CancellationToken cancellationToken = default);
    }
}
