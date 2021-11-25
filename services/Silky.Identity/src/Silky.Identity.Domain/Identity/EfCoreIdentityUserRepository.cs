using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Silky.Core.DependencyInjection;
using Silky.EntityFrameworkCore.Repositories;

namespace Silky.Identity.Domain;

public class EfCoreIdentityUserRepository : EFCoreRepository<IdentityUser>, IIdentityUserRepository, IScopedDependency
{
    public Task<IdentityUser> FindByNormalizedUserNameAsync(string normalizedUserName, bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> GetRoleNamesAsync(long id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> GetRoleNamesInOrganizationUnitAsync(long id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityUser> FindByLoginAsync(string loginProvider, string providerKey, bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityUser> FindByNormalizedEmailAsync(string normalizedEmail, bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<IdentityUser>> GetListByClaimAsync(Claim claim, bool includeDetails = false, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<IdentityUser>> GetListByNormalizedRoleNameAsync(string normalizedRoleName, bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<IdentityUser>> GetListAsync(string sorting = null, int maxResultCount = Int32.MaxValue, int skipCount = 0, string filter = null,
        bool includeDetails = false, Guid? roleId = null, Guid? organizationUnitId = null, string userName = null,
        string phoneNumber = null, string emailAddress = null, bool? isLockedOut = null, bool? notActive = null,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<IdentityRole>> GetRolesAsync(Guid id, bool includeDetails = false, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<IdentityUser>> GetUsersInOrganizationUnitAsync(Guid organizationUnitId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<IdentityUser>> GetUsersInOrganizationsListAsync(List<Guid> organizationUnitIds, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<IdentityUser>> GetUsersInOrganizationUnitWithChildrenAsync(string code, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<long> GetCountAsync(string filter = null, Guid? roleId = null, Guid? organizationUnitId = null, string userName = null,
        string phoneNumber = null, string emailAddress = null, bool? isLockedOut = null, bool? notActive = null,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task EnsureCollectionLoadedAsync<TProperty>(IdentityUser entity, Expression<Func<IdentityUser, IEnumerable<TProperty>>> propertyExpression,
        CancellationToken cancellationToken = default) where TProperty : class
    {
        throw new NotImplementedException();
    }

    public Task EnsureCollectionLoadedAsync(IdentityUser user, Func<object, object> func, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}