using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Silky.EntityFrameworkCore.Repositories;

namespace Silky.Identity.Domain;

public interface IIdentityUserRepository : IRepository<IdentityUser>
{
    Task<IdentityUser> FindByNormalizedUserNameAsync(
        [NotNull] string normalizedUserName,
        bool includeDetails = true,
        CancellationToken cancellationToken = default
    );

    Task<List<string>> GetRoleNamesAsync(
        long id,
        CancellationToken cancellationToken = default
    );
    
    IQueryable<IdentityRole> GetRolesAsync(
        long id,
        bool onlyValid = true,
        CancellationToken cancellationToken = default
    );
    
    Task<IdentityUser> FindByLoginAsync(
        [NotNull] string loginProvider,
        [NotNull] string providerKey,
        bool includeDetails = true,
        CancellationToken cancellationToken = default
    );

    Task<IdentityUser> FindByNormalizedEmailAsync(
        [NotNull] string normalizedEmail,
        bool includeDetails = true,
        CancellationToken cancellationToken = default
    );

    Task<List<IdentityUser>> GetListByClaimAsync(
        Claim claim,
        bool includeDetails = false,
        CancellationToken cancellationToken = default
    );

    Task<List<IdentityUser>> GetListByNormalizedRoleNameAsync(
        string normalizedRoleName,
        bool includeDetails = false,
        CancellationToken cancellationToken = default
    );
    
    Task EnsureCollectionLoadedAsync<TProperty>(
        IdentityUser entity,
        Expression<Func<IdentityUser, IEnumerable<TProperty>>> propertyExpression,
        CancellationToken cancellationToken = default)
        where TProperty : class;

    Task<IdentityUser> FindByPhoneNumberAsync(string phoneNumber, bool includeDetails,
        CancellationToken cancellationToken);

    Task<IdentityUser> FindByAccountAsync(string account, long? tenantId, bool includeDetails,
        CancellationToken cancellationToken);

    Task<IdentityUser> FindByJobNumberAsync(string jobNumber, bool includeDetails, CancellationToken cancellationToken);
}