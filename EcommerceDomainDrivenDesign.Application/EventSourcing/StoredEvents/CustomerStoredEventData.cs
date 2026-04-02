using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceDomainDrivenDesign.Application.EventSourcing.StoredEvents
{
    public class CustomerStoredEventData : StoredEventData
    {
        public string Name { get; set; }
    }
}
