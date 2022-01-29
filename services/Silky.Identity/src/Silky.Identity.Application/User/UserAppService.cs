using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Silky.Core.DbContext.UnitOfWork;
using Silky.Core.Exceptions;
using Silky.Core.Extensions;
using Silky.Core.Runtime.Session;
using Silky.Hero.Common.EntityFrameworkCore;
using Silky.Hero.Common.Extensions;
using Silky.Identity.Application.Contracts.User;
using Silky.Identity.Application.Contracts.User.Dtos;
using Silky.Identity.Domain;
using Silky.Position.Application.Contracts.Position;
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
    public async Task CreateAsync(CreateUserInput input)
    {
        var user = new IdentityUser(input.UserName, input.Email, input.MobilePhone, _session.TenantId);
        await UpdateUserByInput(user, input);

        (await UserManager.CreateAsync(user, input.Password)).CheckErrors();
    }

    public async Task UpdateAsync(UpdateUserInput input)
    {
        var user = await UserManager.GetByIdAsync(input.Id);
        await UpdateUserByInput(user, input);

        if (!input.Password.IsNullOrEmpty())
        {
            (await UserManager.RemovePasswordAsync(user)).CheckErrors();
            (await UserManager.AddPasswordAsync(user, input.Password)).CheckErrors();
        }

        (await UserManager.UpdateAsync(user)).CheckErrors();
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
        await _distributedCache.RemoveAsync(CacheKeyConsts.CurrentUserDataRangeCacheKey,
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

    public Task<PagedList<GetAddOrganizationUserPageOutput>> GetAddOrganizationUserPageAsync(long organizationId, GetAddOrganizationUserPageInput input) 
    {
        return UserManager.GetAddOrganizationUserPageAsync(organizationId, input);
    }

    public Task<PagedList<GetOrganizationUserPageOutput>> GetOrganizationUserPageAsync(long organizationId, GetOrganizationUserPageInput input)
    {
        return UserManager.GetOrganizationUserPageAsync(organizationId, input);
    }

    [UnitOfWork]
    public Task UpdateClaimTypesAsync(long userId, ICollection<UpdateClaimTypeInput> inputs)
    {
        return UserManager.UpdateClaimTypesAsync(userId, inputs);
    }

    public async Task SetRolesAsync(long userId, ICollection<string> roleNames)
    {
        var user = await UserManager.GetByIdAsync(userId);
        (await UserManager.SetRolesAsync(user, roleNames)).CheckErrors();
        (await UserManager.UpdateAsync(user)).CheckErrors();
        await _distributedCache.RemoveAsync(CacheKeyConsts.CurrentUserCacheName,
            string.Format(CacheKeyConsts.CurrentUserDataRangeCacheKey, userId));
        await _distributedCache.RemoveAsync(CacheKeyConsts.CurrentUserCacheName,
            string.Format(CacheKeyConsts.CurrentUserCacheKey, userId));
    }

    public async Task<GetUserRoleOutput> GetRolesAsync(long userId)
    {
        var user = await UserManager.GetByIdAsync(userId);
        var roleNames = await UserManager.GetRolesAsync(user);
        var output = new GetUserRoleOutput()
        {
            UserId = user.Id,
            RoleNames = roleNames
        };
        return output;
    }

    public async Task LockAsync(long userId, int lockoutSeconds)
    {
        var user = await UserManager.GetByIdAsync(userId);
        await UserManager.SetLockoutEnabledAsync(user, true);
        await UserManager.SetLockoutEndDateAsync(user, DateTimeOffset.Now.AddSeconds(lockoutSeconds));
        await UserManager.UpdateAsync(user);
    }

    public async Task UnLockAsync(long userId)
    {
        var user = await UserManager.GetByIdAsync(userId);
        await UserManager.SetLockoutEndDateAsync(user, null);
        await UserManager.UpdateAsync(user);
    }

    public async Task ChangePasswordAsync(long userId, ChangePasswordInput input)
    {
        var user = await UserManager.GetByIdAsync(userId);
        (await UserManager.RemovePasswordAsync(user)).CheckErrors();
        (await UserManager.AddPasswordAsync(user, input.NewPassword)).CheckErrors();
        await UserManager.UpdateAsync(user);
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

    public async Task<ICollection<long>> GetRoleIdsAsync(long userId)
    {
        var user = await UserManager.UserRepository.Include(p => p.Roles).FirstAsync(p => p.Id == userId);
        return user.Roles.Select(p => p.RoleId).ToList();
    }

    protected virtual async Task UpdateUserByInput(IdentityUser user, UserDtoBase input)
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
                .Select(us => new UserSubsidiary(user.Id, us.OrganizationId, us.PositionId, user.TenantId)).ToList();
            (await UserManager.SetUserOrganizations(user, userSubsidiaries)).CheckErrors();
        }
    }

 
    public async Task<ICollection<long>> GetUserIdsAsync(long organizationId)
    {
        var organizationUserIds = await UserManager.UserRepository
            .AsQueryable(false)
            .Include(p=> p.UserSubsidiaries)
            .Where(p => p.UserSubsidiaries.Any(us=> us.OrganizationId == organizationId))
            .Select(p=> p.Id).ToListAsync();
        return organizationUserIds;
    }

    [UnitOfWork]
    public async Task AddOrganizationUsers(long organizationId, ICollection<AddOrganizationUserInput> inputs)
    {
        foreach (var input in inputs)
        {
            var user = await UserManager.UserRepository.Include(p=> p.UserSubsidiaries).FirstOrDefaultAsync(p=> p.Id == input.UserId);
            if (user == null) 
            {
                throw new EntityNotFoundException(typeof(IdentityUser),input.UserId);
            }
            user.AddUserSubsidiaries(organizationId, input.PositionId);
            await UserManager.UpdateAsync(user);
        }
    }

    //public async Task<GetUserPositionOutput> GetUserPositionInfo(long userId, long organizationId)
    //{
    //    var userSubsidiary = await UserManager.UserSubsidiaryRepository.FirstOrDefaultAsync(p => p.UserId == userId && p.OrganizationId == organizationId);
    //    if (userSubsidiary != null) 
    //    {
    //        var positionInfo = await _positionAppService.GetAsync(userSubsidiary.PositionId);
    //        return positionInfo.Adapt<GetUserPositionOutput>();
    //    }
    //    return null;
    //}
}