using EcommerceDomainDrivenDesign.Domain.Core.Messaging;
using System;

namespace EcommerceDomainDrivenDesign.Domain.Customers.Events
{
    public class CustomerRegisteredEvent : Event
    {
        public Guid CustomerId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        public CustomerRegisteredEvent(Guid customerId, string name, string email)
        {
            CustomerId = customerId;
            Name = name;
            Email = email;
            AggregateId = CustomerId;
        }
    }
}
