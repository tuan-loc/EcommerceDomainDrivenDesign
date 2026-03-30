using System.Threading;
using System.Threading.Tasks;

namespace EcommerceDomainDrivenDesign.Domain.Core.Base
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
