using System;
using System.Collections.Generic;
using System.Text;

namespace EcommerceDomainDrivenDesign.Infrastructure.Identity.Helpers
{
    public interface IUserProvider
    {
        Guid GetUserId();
    }
}
