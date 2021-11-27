using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Silky.Core.DependencyInjection;
using Silky.EntityFrameworkCore.Repositories;

namespace Silky.Identity.Domain;

public class EfCoreIdentityUserRepository : EFCoreRepository<IdentityUser>, IIdentityUserRepository, IScopedDependency
{
    public Task<IdentityUser> FindByNormalizedUserNameAsync(string normalizedUserName, bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        if (includeDetails)
        {
            return Entities
                .Include(p => p.Claims)
                .Include(p => p.Logins)
                .Include(p => p.Roles)
                .Include(p => p.Tokens)
                .Include(p => p.UserSubsidiaries)
                .FirstOrDefaultAsync(p => p.NormalizedUserName == normalizedUserName && !p.IsDeleted);
        }
        return FirstOrDefaultAsync(p => p.NormalizedUserName == normalizedUserName);
    }

    public async Task<List<string>> GetRoleNamesAsync(long id, CancellationToken cancellationToken = default)
    {
        var query = from userRole in Context.Set<IdentityUserRole>()
            join role in Context.Set<IdentityRole>() on userRole.RoleId equals role.Id 
            where userRole.UserId == id && !role.IsDeleted
            select role.Name;

        return await query.ToListAsync(cancellationToken);

    }
    

    public Task<IdentityUser> FindByLoginAsync(string loginProvider, string providerKey, bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        if (includeDetails)
        {
            return Entities
                .Include(p => p.Claims)
                .Include(p => p.Logins)
                .Include(p => p.Roles)
                .Include(p => p.Tokens)
                .Include(p => p.UserSubsidiaries)
                .Where(u => u.Logins.Any(login =>
                    login.LoginProvider == loginProvider && login.ProviderKey == providerKey) && !u.IsDeleted)
                .OrderBy(x => x.Id)
                .FirstOrDefaultAsync(cancellationToken);
        }
        return Entities
            .Where(u => u.Logins.Any(login =>
                login.LoginProvider == loginProvider && login.ProviderKey == providerKey) && !u.IsDeleted)
            .OrderBy(x => x.Id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<IdentityUser> FindByNormalizedEmailAsync(string normalizedEmail, bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        
        if (includeDetails)
        {
            return Entities
                .Include(p => p.Claims)
                .Include(p => p.Logins)
                .Include(p => p.Roles)
                .Include(p => p.Tokens)
                .Include(p => p.UserSubsidiaries)
                .FirstOrDefaultAsync(p => p.NormalizedEmail == normalizedEmail && !p.IsDeleted);
        }
        return FirstOrDefaultAsync(p => p.NormalizedEmail == normalizedEmail);
    }

    public Task<List<IdentityUser>> GetListByClaimAsync(Claim claim, bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        if (includeDetails)
        {
            return Entities
                .Include(p => p.Claims)
                .Include(p => p.Logins)
                .Include(p => p.Roles)
                .Include(p => p.Tokens)
                .Include(p => p.UserSubsidiaries)
                .Where(u => u.Claims.Any(c =>
                    c.ClaimType == claim.Type && c.ClaimValue == claim.Value) && !u.IsDeleted)
                .ToListAsync(cancellationToken);
        }
        return Entities
            .Where(u => u.Claims.Any(c =>
                c.ClaimType == claim.Type && c.ClaimValue == claim.Value) && !u.IsDeleted)
            .ToListAsync(cancellationToken);
    }
    
    public Task<IdentityUser> FindByPhoneNumberAsync(string phoneNumber, bool includeDetails, CancellationToken cancellationToken)
    {
        if (includeDetails)
        {
            return Entities
                .Include(p => p.Claims)
                .Include(p => p.Logins)
                .Include(p => p.Roles)
                .Include(p => p.Tokens)
                .Include(p => p.UserSubsidiaries)
                .FirstOrDefaultAsync(p => p.MobilePhone == phoneNumber && !p.IsDeleted);
        }
        return FirstOrDefaultAsync(p => p.MobilePhone == phoneNumber);
    }

    public Task<List<IdentityUser>> GetListByNormalizedRoleNameAsync(string normalizedRoleName,
        bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<IdentityUser>> GetListAsync(string sorting = null, int maxResultCount = Int32.MaxValue,
        int skipCount = 0, string filter = null,
        bool includeDetails = false, Guid? roleId = null, Guid? organizationUnitId = null, string userName = null,
        string phoneNumber = null, string emailAddress = null, bool? isLockedOut = null, bool? notActive = null,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<IdentityRole>> GetRolesAsync(Guid id, bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<IdentityUser>> GetUsersInOrganizationUnitAsync(Guid organizationUnitId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<IdentityUser>> GetUsersInOrganizationsListAsync(List<Guid> organizationUnitIds,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<IdentityUser>> GetUsersInOrganizationUnitWithChildrenAsync(string code,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<long> GetCountAsync(string filter = null, Guid? roleId = null, Guid? organizationUnitId = null,
        string userName = null,
        string phoneNumber = null, string emailAddress = null, bool? isLockedOut = null, bool? notActive = null,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task EnsureCollectionLoadedAsync<TProperty>(IdentityUser entity,
        Expression<Func<IdentityUser, IEnumerable<TProperty>>> propertyExpression,
        CancellationToken cancellationToken = default) where TProperty : class
    {
        throw new NotImplementedException();
    }

    public Task EnsureCollectionLoadedAsync(IdentityUser user, Func<object, object> func,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}