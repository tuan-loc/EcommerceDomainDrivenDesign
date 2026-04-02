using EcommerceDomainDrivenDesign.Domain.Core.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceDomainDrivenDesign.Domain.Payments.Events
{
    public class PaymentCreatedEvent : Event
    {
        public Guid PaymentId { get; private set; }

        public PaymentCreatedEvent(Guid paymentId)
        {
            PaymentId = paymentId;
            AggregateId = paymentId;
        }
    }
}
