using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDomainDrivenDesign.Infrastructure.Identity.Services
{
    public interface IJwtService
    {
        Task<string> GenerateJwt(string email);
    }
}
