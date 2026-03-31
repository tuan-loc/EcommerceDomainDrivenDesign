using System.Threading.Tasks;

namespace EcommerceDomainDrivenDesign.Infrastructure.Identity.Services
{
    public interface IJwtService
    {
        Task<string> GenerateJwt(string email);
    }
}
