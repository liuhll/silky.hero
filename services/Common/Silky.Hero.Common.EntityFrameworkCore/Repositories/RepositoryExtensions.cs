using Silky.Account.Application.Contracts.Account;
using Silky.Core;
using Silky.EntityFrameworkCore.Entities;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Hero.Common.EntityFrameworkCore.Entities;

namespace Silky.Hero.Common.EntityFrameworkCore.Repositories;

public static class RepositoryExtensions
{
    public static async Task<IQueryable<TEntity>> GetCurrentUserDataRanges<TEntity>(IQueryable<TEntity> query)
        where TEntity : class, IHasOrganization, IEntity, new()
    {
        var accountAppService = EngineContext.Current.Resolve<IAccountAppService>();
        var currentUserDataRange = await accountAppService.GetCurrentUserDataRangeAsync();
        if (currentUserDataRange.IsAllData)
        {
            return query;
        }

        if (currentUserDataRange.OrganizationIds.Count() == 1)
        {
            return query.Where(p => p.OrganizationId == currentUserDataRange.OrganizationIds.First());
        }

        return query.Where(p => currentUserDataRange.OrganizationIds.Contains(p.OrganizationId));
    }

    public static async Task<IQueryable<TEntity>> GetCurrentUserDataRanges<TEntity>(IRepository<TEntity> repository)
        where TEntity : class, IHasOrganization, IEntity, new()
    {
        var accountAppService = EngineContext.Current.Resolve<IAccountAppService>();
        var currentUserDataRange = await accountAppService.GetCurrentUserDataRangeAsync();
        var query = repository.AsQueryable(false);
        if (currentUserDataRange.IsAllData)
        {
            return query;
        }
        if (currentUserDataRange.OrganizationIds.Count() == 1)
        {
            return query.Where(p => p.OrganizationId == currentUserDataRange.OrganizationIds.First());
        }
        return query.Where(p => currentUserDataRange.OrganizationIds.Contains(p.OrganizationId));
    }
}