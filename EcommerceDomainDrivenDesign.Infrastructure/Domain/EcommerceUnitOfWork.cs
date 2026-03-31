using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EcommerceDomainDrivenDesign.Domain;
using EcommerceDomainDrivenDesign.Domain.Core.Base;
using EcommerceDomainDrivenDesign.Domain.Core.Messaging;
using EcommerceDomainDrivenDesign.Domain.Customers;
using EcommerceDomainDrivenDesign.Domain.Products;
using EcommerceDomainDrivenDesign.Infrastructure.Database.Context;
using EcommerceDomainDrivenDesign.Infrastructure.Domain;
using EcommerceDomainDrivenDesign.Infrastructure.Messaging;

namespace EcommerceDomainDrivenDesign.Infrastructure.Domain
{
    public class EcommerceUnitOfWork : UnitOfWork<EcommerceDDDContext>, IEcommerceUnitOfWork
    {
        public ICustomerRepository CustomerRepository { get; }
        public IStoredEventRepository MessageRepository { get; }
        public IProductRepository ProductRepository { get; }

        private readonly IEventSerializer _eventSerializer;

        public EcommerceUnitOfWork(EcommerceDDDContext dbContext,
            ICustomerRepository customerRepository,
            IStoredEventRepository messageRepository,
            IProductRepository productRepository,
            IEventSerializer eventSerializer) : base(dbContext)
        {
            CustomerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            MessageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            ProductRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));

            _eventSerializer = eventSerializer ?? throw new ArgumentNullException(nameof(eventSerializer));
        }

        protected async override Task StoreEvents(CancellationToken cancellationToken)
        {
            var entities = DbContext.ChangeTracker.Entries()
                    .Where(e => e.Entity is Entity c && c.DomainEvents != null)
                    .Select(e => e.Entity as Entity)
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
