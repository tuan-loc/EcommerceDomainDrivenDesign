using MediatR;
using System;

namespace EcommerceDomainDrivenDesign.Domain.Core.Messaging
{
    public abstract class Event : Message, INotification
    {
        public DateTime CreatedAt { get; private set; }
        protected Event()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
