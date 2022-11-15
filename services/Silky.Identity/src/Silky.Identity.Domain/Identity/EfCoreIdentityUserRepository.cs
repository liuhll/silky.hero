using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Silky.Core;
using Silky.Core.DependencyInjection;
using Silky.Core.Runtime.Session;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Hero.Common.Enums;

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
                .FirstOrDefaultAsync(p => p.NormalizedUserName == normalizedUserName);
        }

        return FirstOrDefaultAsync(p => p.NormalizedUserName == normalizedUserName);
    }

    public async Task<List<string>> GetRoleNamesAsync(long id, CancellationToken cancellationToken = default)
    {
        var query = from userRole in Context.Set<IdentityUserRole>()
            join role in Context.Set<IdentityRole>() on userRole.RoleId equals role.Id
            where userRole.UserId == id
            select role.Name;

        return await query.ToListAsync(cancellationToken);
    }

    public IQueryable<IdentityRole> GetRolesAsync(long id,  bool onlyValid = true, CancellationToken cancellationToken = default)
    {
        var query = from userRole in Context.Set<IdentityUserRole>()
            join role in Context.Set<IdentityRole>() on userRole.RoleId equals role.Id
            where userRole.UserId == id
            select role;

        if (onlyValid)
        {
            return query.Where(p => p.Status == Status.Valid);
        }
        return query;
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
                    login.LoginProvider == loginProvider && login.ProviderKey == providerKey))
                .OrderBy(x => x.Id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        return Entities
            .Where(u => u.Logins.Any(login =>
                login.LoginProvider == loginProvider && login.ProviderKey == providerKey))
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
                .FirstOrDefaultAsync(p => p.NormalizedEmail == normalizedEmail);
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
                    c.ClaimType == claim.Type && c.ClaimValue == claim.Value))
                .ToListAsync(cancellationToken);
        }

        return Entities
            .Where(u => u.Claims.Any(c =>
                c.ClaimType == claim.Type && c.ClaimValue == claim.Value))
            .ToListAsync(cancellationToken);
    }

    public Task<IdentityUser> FindByPhoneNumberAsync(string phoneNumber, bool includeDetails,
        CancellationToken cancellationToken)
    {
        if (includeDetails)
        {
            return Entities
                .Include(p => p.Claims)
                .Include(p => p.Logins)
                .Include(p => p.Roles)
                .Include(p => p.Tokens)
                .Include(p => p.UserSubsidiaries)
                .FirstOrDefaultAsync(p => p.MobilePhone == phoneNumber);
        }

        return FirstOrDefaultAsync(p => p.MobilePhone == phoneNumber);
    }

    public Task<IdentityUser> FindByAccountAsync(string account, long? tenantId, bool includeDetails,
        CancellationToken cancellationToken)
    {
        var currentTenantId = EngineContext.Current.Resolve<ICurrentTenantId>();
        currentTenantId.SetTenantId(tenantId);
        if (includeDetails)
        {
            return Entities
                .Include(p => p.Claims)
                .FirstOrDefaultAsync(p =>
                    p.NormalizedUserName == account || p.MobilePhone == account || p.NormalizedEmail == account);
        }

        return FirstOrDefaultAsync(p =>
            p.NormalizedUserName == account || p.MobilePhone == account || p.NormalizedEmail == account);
    }

    public Task<IdentityUser> FindByJobNumberAsync(string jobNumber, bool includeDetails, CancellationToken cancellationToken)
    {
        if (includeDetails)
        {
            return Entities
                .Include(p => p.Claims)
                .Include(p => p.Logins)
                .Include(p => p.Roles)
                .Include(p => p.Tokens)
                .Include(p => p.UserSubsidiaries)
                .FirstOrDefaultAsync(p => p.JobNumber == jobNumber);
        }

        return FirstOrDefaultAsync(p => p.JobNumber == jobNumber);
    }

    public Task<List<IdentityUser>> GetListByNormalizedRoleNameAsync(string normalizedRoleName,
        bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task EnsureCollectionLoadedAsync<TProperty>(IdentityUser entity,
        Expression<Func<IdentityUser, IEnumerable<TProperty>>> propertyExpression,
        CancellationToken cancellationToken = default) where TProperty : class
    {
        cancellationToken.ThrowIfCancellationRequested();
        entity = await Entities.Include(propertyExpression)
            .FirstAsync(p => p.Id == entity.Id, cancellationToken: cancellationToken);
    }
}