using EcommerceDomainDrivenDesign.Domain.Core.Messaging;
using System.Collections.Generic;

namespace EcommerceDomainDrivenDesign.Domain.Core.Base
{
    /// <summary>
    ///  Aggregate root interface
    /// </summary>
    public interface IAggregateRoot
    {
        IReadOnlyCollection<Event> DomainEvents { get; }
        void ClearDomainEvents();
    }
}
