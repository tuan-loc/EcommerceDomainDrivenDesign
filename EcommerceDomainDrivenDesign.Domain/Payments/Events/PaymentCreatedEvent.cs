using EcommerceDomainDrivenDesign.Domain.Core.Messaging;
using System;

namespace EcommerceDomainDrivenDesign.Domain.Payments.Events
{
    public class PaymentCreatedEvent : Event
    {
        public Guid PaymentId { get; private set; }
        public Guid OrderId { get; private set; }

        public PaymentCreatedEvent(Guid paymentId, Guid orderId)
        {
            PaymentId = paymentId;
            OrderId = orderId;
            AggregateId = paymentId;
        }
    }
}
