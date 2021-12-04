using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Silky.EntityFrameworkCore.Repositories;

namespace Silky.Identity.Domain;

public interface IIdentityRoleRepository : IRepository<IdentityRole>
{
    Task<IdentityRole> FindByNormalizedNameAsync(
        string normalizedRoleName,
        bool includeDetails = true,
        CancellationToken cancellationToken = default
    );

    Task EnsureCollectionLoadedAsync<TProperty>(IdentityRole role,
        Expression<Func<IdentityRole, IEnumerable<TProperty>>> propertyExpression,
        CancellationToken cancellationToken = default)
        where TProperty : class;
}