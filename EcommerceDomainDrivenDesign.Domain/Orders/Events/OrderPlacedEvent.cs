using System;
using EcommerceDomainDrivenDesign.Domain.Core.Messaging;

namespace EcommerceDomainDrivenDesign.Domain.Orders.Events
{
    public class OrderPlacedEvent : Event
    {
        public Guid OrderId { get; private set; }

        public OrderPlacedEvent(Guid orderId)
        {
            OrderId = orderId;
            AggregateId = OrderId;
        }
    }
}
