using EcommerceDomainDrivenDesign.Domain.Core.Messaging;
using System;

namespace EcommerceDomainDrivenDesign.Domain.Customers.Orders.Events
{
    public class OrderChangedEvent : Event
    {
        public Guid OrderId { get; private set; }

        public OrderChangedEvent(Guid orderId)
        {
            OrderId = orderId;
            AggregateId = orderId;
        }
    }
}
