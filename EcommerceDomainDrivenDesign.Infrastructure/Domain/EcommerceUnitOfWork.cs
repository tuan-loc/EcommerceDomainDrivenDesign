using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EcommerceDomainDrivenDesign.Domain;
using EcommerceDomainDrivenDesign.Domain.Carts;
using EcommerceDomainDrivenDesign.Domain.Core.Base;
using EcommerceDomainDrivenDesign.Domain.Core.Messaging;
using EcommerceDomainDrivenDesign.Domain.Customers;
using EcommerceDomainDrivenDesign.Domain.Orders;
using EcommerceDomainDrivenDesign.Domain.Payments;
using EcommerceDomainDrivenDesign.Domain.Products;
using EcommerceDomainDrivenDesign.Infrastructure.Database.Context;
using EcommerceDomainDrivenDesign.Infrastructure.Messaging;

namespace EcommerceDomainDrivenDesign.Infrastructure.Domain
{
    public class EcommerceUnitOfWork : UnitOfWork<EcommerceDomainDrivenDesignContext>, IEcommerceUnitOfWork
    {
        public ICustomerRepository CustomerRepository { get; }
        public IOrderRepository OrderRepository { get; }
        public IStoredEventRepository MessageRepository { get; }
        public IProductRepository ProductRepository { get; }
        public ICartRepository CartRepository { get; }
        public IPaymentRepository PaymentRepository { get; }

        private readonly IEventSerializer _eventSerializer;

        public EcommerceUnitOfWork(EcommerceDomainDrivenDesignContext dbContext,
            ICustomerRepository customerRepository,
            IOrderRepository orderRepository,
            IStoredEventRepository messageRepository,
            IProductRepository productRepository,
            IPaymentRepository paymentRepository,
            ICartRepository cartRepository,
            IEventSerializer eventSerializer) : base(dbContext)
        {
            CustomerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            OrderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            MessageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            ProductRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            CartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
            PaymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));

            _eventSerializer = eventSerializer ?? throw new ArgumentNullException(nameof(eventSerializer));
        }

        protected async override Task StoreEvents(CancellationToken cancellationToken)
        {
            var entities = DbContext.ChangeTracker.Entries()
                    .Where(e => e.Entity is AggregateRoot<Guid> c && c.DomainEvents != null)
                    .Select(e => e.Entity as AggregateRoot<Guid>)
                    .ToArray();

            foreach (var entity in entities)
            {
                var messages = entity.DomainEvents
                    .Select(e => 
                    StoredEventHelper.BuildFromDomainEvent(e, _eventSerializer)).ToArray();

                entity.ClearDomainEvents();
                await DbContext.AddRangeAsync(messages, cancellationToken);
            }
        }
    }
}
