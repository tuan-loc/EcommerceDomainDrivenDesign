using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EcommerceDomainDrivenDesign.Infrastructure.Identity.IdentityUser
{
    public class User : IdentityUser<Guid>, IUser
    {
        public string Name => GetName();

        private readonly IHttpContextAccessor _accessor;

        private User()
        {
        }

        public User(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        private string GetName()
        {
            return _accessor.HttpContext.User.Identity.Name ??
                   _accessor.HttpContext.User.Claims
                   .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }
    }

    public class Role : IdentityRole<Guid> { }
}
