using System;
using EcommerceDomainDrivenDesign.Domain.Core.Messaging;

namespace EcommerceDomainDrivenDesign.Domain.Customers.Events
{
    public class CustomerRegisteredEvent : Event
    {
        public Guid CustomerId { get; private set; }
        public string Name { get; private set; }

        public CustomerRegisteredEvent(Guid customerId, string name)
        {
            CustomerId = customerId;
            Name = name;
            AggregateId = CustomerId;
        }
    }
}
