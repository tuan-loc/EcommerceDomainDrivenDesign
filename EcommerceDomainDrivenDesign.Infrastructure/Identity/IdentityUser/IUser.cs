using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace EcommerceDomainDrivenDesign.Infrastructure.Identity.IdentityUser
{
    public interface IUser
    {
        string Name { get; }
        string Email { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
