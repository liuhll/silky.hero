using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Silky.Core.DependencyInjection;
using Silky.EntityFrameworkCore.Repositories;

namespace Silky.Identity.Domain;

public class EfCoreIdentityRoleRepository : EFCoreRepository<IdentityRole>, IIdentityRoleRepository, IScopedDependency
{
    public Task<IdentityRole> FindByNormalizedNameAsync(string normalizedRoleName, bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<IdentityRole>> GetListAsync(string sorting = null, int maxResultCount = Int32.MaxValue,
        int skipCount = 0, string filter = null,
        bool includeDetails = false, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<IdentityRole>> GetListAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<IdentityRole>> GetDefaultOnesAsync(bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<long> GetCountAsync(string filter = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}