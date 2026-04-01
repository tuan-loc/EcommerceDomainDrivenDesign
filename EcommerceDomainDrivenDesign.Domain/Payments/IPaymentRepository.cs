using System;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerceDomainDrivenDesign.Domain.Payments
{
    public interface IPaymentRepository
    {
        public Task AddPayment(Payment payment, CancellationToken cancellationToken = default);
        public Task<Payment> GetPaymentById(Guid id, CancellationToken cancellationToken = default);
    }
}
