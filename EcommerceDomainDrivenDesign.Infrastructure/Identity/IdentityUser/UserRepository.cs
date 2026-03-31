using System.Threading.Tasks;
using EcommerceDomainDrivenDesign.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace EcommerceDomainDrivenDesign.Infrastructure.Identity.IdentityUser
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityContext _dbContext;

        public UserRepository(IdentityContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(c => c.Email == email);
        }
    }
}
