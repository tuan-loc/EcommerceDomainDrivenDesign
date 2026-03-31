using System;

namespace EcommerceDomainDrivenDesign.Infrastructure.Identity.Helpers
{
    public interface IUserProvider
    {
        Guid GetUserId();
    }
}
