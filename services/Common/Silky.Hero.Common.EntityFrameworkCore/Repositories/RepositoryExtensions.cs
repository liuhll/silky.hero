using Microsoft.EntityFrameworkCore;
using Silky.Account.Application.Contracts.Account;
using Silky.Core;
using Silky.Core.Threading;
using Silky.EntityFrameworkCore.Entities;
using Silky.Hero.Common.EntityFrameworkCore.Entities;
using Silky.Hero.Common.Session;

namespace Silky.EntityFrameworkCore.Repositories;

public static class RepositoryExtensions
{
    public static IQueryable<TEntity> GetCurrentUserDataRanges<TEntity>(
        this IQueryable<TEntity> query, bool? tracking = false)
        where TEntity : class, IHasOrganization, IEntity, new()
    {
        var accountAppService = EngineContext.Current.Resolve<IAccountAppService>();
        var currentUserDataRange = AsyncHelper.RunSync(() => accountAppService.GetCurrentUserDataRangeAsync());
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

    public static IQueryable<TEntity> GetCurrentUserDataRanges<TEntity>(
        this IRepository<TEntity> repository, bool? tracking = false)
        where TEntity : class, IHasOrganization, IEntity, new()
    {
        return repository.AsQueryable(tracking).GetCurrentUserDataRanges();
    }

    public static Task RealDeleteAsync<TEntity>(this IRepository<TEntity> repository, IEnumerable<TEntity> entities)
        where TEntity : class, IEntity, new()
    {
        repository.DynamicContext.RealDeleteFlag = true;
        return repository.DeleteAsync(entities);
    }

    public static Task RealDeleteAsync<TEntity>(this IRepository<TEntity> repository, TEntity entity)
        where TEntity : class, IEntity, new()
    {
        repository.DynamicContext.RealDeleteFlag = true;
        return repository.DeleteAsync(entity);
    }

    public static Task RealDeleteAsync<TEntity>(this IRepository<TEntity> repository, object key)
        where TEntity : class, IEntity, new()
    {
        repository.DynamicContext.RealDeleteFlag = true;
        return repository.DeleteAsync(key);
    }

    public static Task RealDeleteNowAsync<TEntity>(this IRepository<TEntity> repository, IEnumerable<TEntity> entities)
        where TEntity : class, IEntity, new()
    {
        repository.DynamicContext.RealDeleteFlag = true;
        return repository.DeleteNowAsync(entities);
    }

    public static Task RealDeleteNowAsync<TEntity>(this IRepository<TEntity> repository, TEntity entity)
        where TEntity : class, IEntity, new()
    {
        repository.DynamicContext.RealDeleteFlag = true;
        return repository.DeleteNowAsync(entity);
    }

    public static Task RealDeleteNowAsync<TEntity>(this IRepository<TEntity> repository, object key)
        where TEntity : class, IEntity, new()
    {
        repository.DynamicContext.RealDeleteFlag = true;
        return repository.DeleteNowAsync(key);
    }

    public static IQueryable<TEntity> GetAllIncludeSoftDeletedAsync<TEntity>(this IRepository<TEntity> repository,
        bool? tracking = false)
        where TEntity : class, IEntity, new()
    {
        var query = repository.AsQueryable(tracking).IgnoreQueryFilters();

        if (typeof(IHasTenantObject).IsAssignableFrom(typeof(TEntity)))
        {
            query = query.Where(e => EF.Property<object>(e, nameof(IHasTenantObject.TenantId)) == GetTenantId());
        }

        return query;
    }

    private static object GetTenantId()
    {
        var currentTenantId = EngineContext.Current.Resolve<ICurrentTenantId>();
        return currentTenantId.TenantId;
    }
}