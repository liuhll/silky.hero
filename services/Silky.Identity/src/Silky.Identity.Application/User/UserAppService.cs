using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Silky.Core.DbContext.UnitOfWork;
using Silky.Core.Exceptions;
using Silky.Core.Extensions;
using Silky.Hero.Common.Extensions;
using Silky.Identity.Application.Contracts.User;
using Silky.Identity.Application.Contracts.User.Dtos;
using Silky.Identity.Domain;
using Silky.Rpc.Runtime.Server;
using IdentityUser = Silky.Identity.Domain.IdentityUser;

namespace Silky.Identity.Application.User;

public class UserAppService : IUserAppService
{
    protected IdentityUserManager UserManager { get; private set; }
    private readonly ISession _session;
    private readonly IDistributedCache _distributedCache;

    public UserAppService(IdentityUserManager userManager,
        IDistributedCache distributedCache)
    {
        UserManager = userManager;
        _distributedCache = distributedCache;
        _session = NullSession.Instance;
    }

    [UnitOfWork]
    public async Task CreateOrUpdateAsync(CreateOrUpdateUserInput input)
    {
        var user = !input.Id.HasValue
            ? new IdentityUser(input.UserName, input.Email, input.MobilePhone, _session.TenantId)
            : await UserManager.GetByIdAsync(input.Id.Value);
        await UpdateUserByInput(user, input);

        if (!input.Id.HasValue)
        {
            (await UserManager.CreateAsync(user, input.Password)).CheckErrors();
        }
        else
        {
            if (!input.Password.IsNullOrEmpty())
            {
                (await UserManager.RemovePasswordAsync(user)).CheckErrors();
                (await UserManager.AddPasswordAsync(user, input.Password)).CheckErrors();
            }

            (await UserManager.UpdateAsync(user)).CheckErrors();
        }
    }

    public async Task DeleteAsync(long id)
    {
        var user = await UserManager.GetByIdAsync(id);
        if (user == null)
        {
            throw new UserFriendlyException($"不存在Id为{id}的账号");
        }

        (await UserManager.DeleteAsync(user)).CheckErrors();
        await _distributedCache.RemoveAsync(CacheKeyConsts.CurrentUserCacheName,
            string.Format(CacheKeyConsts.CurrentUserCacheKey, id));
    }

    public async Task<GetUserOutput> GetAsync(long id)
    {
        var user = await UserManager.GetByIdAsync(id, true);
        if (user == null)
        {
            throw new UserFriendlyException($"不存在Id为{id}的账号");
        }

        return user.Adapt<GetUserOutput>();
    }

    public Task<PagedList<GetUserPageOutput>> GetPageAsync(GetUserPageInput input)
    {
        return UserManager.GetPageAsync(input);
    }

    public Task<ICollection<GetUserOutput>> GetOrganizationUsersAsync(long organizationId)
    {
        return UserManager.GetOrganizationUsersAsync(organizationId);
    }

    public Task<bool> HasOrganizationUsersAsync(long organizationId)
    {
        return UserManager.HasOrganizationUsersAsync(organizationId);
    }

    public Task<bool> HasPositionUsersAsync(long positionId)
    {
        return UserManager.HasPositionUsersAsync(positionId);
    }

    protected virtual async Task UpdateUserByInput(IdentityUser user, CreateOrUpdateUserInput input)
    {
        if (!string.Equals(user.Email, input.Email, StringComparison.InvariantCultureIgnoreCase))
        {
            (await UserManager.SetEmailAsync(user, input.Email)).CheckErrors();
        }

        if (!string.Equals(user.MobilePhone, input.MobilePhone, StringComparison.InvariantCultureIgnoreCase))
        {
            (await UserManager.SetPhoneNumberAsync(user, input.MobilePhone)).CheckErrors();
        }

        (await UserManager.SetLockoutEnabledAsync(user, input.LockoutEnabled)).CheckErrors();

        user.UserName = input.UserName;
        user.Surname = input.Surname;
        user.RealName = input.RealName;
        user.JobNumber = input.JobNumber;
        user.Sex = input.Sex;
        user.BirthDay = input.BirthDay;
        user.TelPhone = input.TelPhone;

        if (input.UserSubsidiaries != null)
        {
            var userSubsidiaries = input.UserSubsidiaries
                .Select(us => new UserOrganization(user.Id, us.OrganizationId, us.PositionId, user.TenantId)).ToList();
            (await UserManager.SetUserOrganizations(user, userSubsidiaries)).CheckErrors();
        }
    }
}