using EcommerceDomainDrivenDesign.Domain.Core.Messaging;
using System;

namespace EcommerceDomainDrivenDesign.Domain.Customers.Orders.Events
{
    public class OrderPlacedEvent : Event
    {
        public Guid CustomerId { get; private set; }
        public Guid OrderId { get; private set; }

        public OrderPlacedEvent(
            Guid customerId,
            Guid orderId)
        {
            CustomerId = customerId;
            OrderId = orderId;
            AggregateId = OrderId;
        }
    }
}
