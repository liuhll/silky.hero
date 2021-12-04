using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Silky.Core.DependencyInjection;
using Silky.EntityFrameworkCore.Repositories;

namespace Silky.Identity.Domain;

public class EfCoreIdentityRoleRepository : EFCoreRepository<IdentityRole>, IIdentityRoleRepository, IScopedDependency
{
    public Task<IdentityRole> FindByNormalizedNameAsync(string normalizedRoleName, bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        if (includeDetails)
        {
            return Entities
                .Include(p => p.Claims)
                .FirstOrDefaultAsync(p => p.NormalizedName == normalizedRoleName && !p.IsDeleted);
        }

        return FirstOrDefaultAsync(p => p.NormalizedName == normalizedRoleName);
    }

    public async Task EnsureCollectionLoadedAsync<TProperty>(IdentityRole role,
        Expression<Func<IdentityRole, IEnumerable<TProperty>>> propertyExpression,
        CancellationToken cancellationToken = default) where TProperty : class
    {
        cancellationToken.ThrowIfCancellationRequested();
        role = await Entities.Include(propertyExpression)
            .FirstAsync(p => p.Id == role.Id, cancellationToken: cancellationToken);
    }
}