using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EcommerceDomainDrivenDesign.Domain.Core.Messaging;

namespace EcommerceDomainDrivenDesign.Infrastructure.Messaging
{
    public interface IMessagePublisher
    {
        Task Publish(StoredEvent message, System.Threading.CancellationToken cancellationToken);
    }
}
