using System.Threading.Tasks;

namespace EcommerceDomainDrivenDesign.Infrastructure.Identity.IdentityUser
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
    }
}
