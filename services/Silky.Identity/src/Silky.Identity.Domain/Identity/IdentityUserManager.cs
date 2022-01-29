using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Silky.Core;
using Silky.Core.Exceptions;
using Silky.Core.Extensions;
using Silky.EntityFrameworkCore.Extensions;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Hero.Common.EntityFrameworkCore;
using Silky.Identity.Application.Contracts.User.Dtos;
using Silky.Identity.Domain.Shared;
using Silky.Organization.Application.Contracts.Organization;
using Silky.Position.Application.Contracts.Position;

namespace Silky.Identity.Domain;

public class IdentityUserManager : UserManager<IdentityUser>
{
    public IIdentityUserRepository UserRepository { get; }
    public IRepository<UserSubsidiary> UserSubsidiaryRepository { get; }
    public IRepository<IdentityUserClaim> UserClaimRepository { get; }
    public IRepository<IdentityClaimType> ClaimTypeRepository { get; }

    private readonly IRepository<IdentityRoleOrganization> _roleOrganizationRepository;
    private readonly IOrganizationAppService _organizationAppService;
    private readonly IPositionAppService _positionAppService;

    public IdentityUserManager(IdentityUserStore store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<IdentityUser> passwordHasher,
        IEnumerable<IUserValidator<IdentityUser>> userValidators,
        IEnumerable<IPasswordValidator<IdentityUser>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<IdentityUserManager> logger,
        IRepository<UserSubsidiary> userSubsidiaryRepository,
        IIdentityUserRepository userRepository,
        IOrganizationAppService organizationAppService,
        IPositionAppService positionAppService,
        IRepository<IdentityUserClaim> userClaimRepository,
        IRepository<IdentityClaimType> claimTypeRepository,
        IRepository<IdentityRoleOrganization> roleOrganizationRepository)
        : base(store,
            optionsAccessor,
            passwordHasher,
            userValidators,
            passwordValidators,
            keyNormalizer,
            errors,
            services,
            logger)
    {
        UserSubsidiaryRepository = userSubsidiaryRepository;
        UserRepository = userRepository;
        _organizationAppService = organizationAppService;
        _positionAppService = positionAppService;
        UserClaimRepository = userClaimRepository;
        ClaimTypeRepository = claimTypeRepository;
        _roleOrganizationRepository = roleOrganizationRepository;
    }

    public async Task<IdentityUser> GetByIdAsync(long id)
    {
        var user = await Store.FindByIdAsync(id.ToString(), CancellationToken);
        if (user == null)
        {
            throw new EntityNotFoundException(typeof(IdentityUser), id);
        }

        return user;
    }


    public async Task<IdentityResult> SetUserOrganizations(IdentityUser user,
        ICollection<UserSubsidiary> userSubsidiaries)
    {
        Check.NotNull(user, nameof(user));
        Check.NotNull(userSubsidiaries, nameof(userSubsidiaries));

        foreach (var userSubsidiary in userSubsidiaries)
        {
            if (!await _organizationAppService.HasOrganizationAsync(userSubsidiary.OrganizationId))
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Code = "NoOrganization",
                    Description = $"不存在Id为{userSubsidiary.OrganizationId}的组织机构"
                });
            }

            if (!await _positionAppService.HasPositionAsync(userSubsidiary.PositionId))
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Code = "NoPosition",
                    Description = $"不存在Id为{userSubsidiary.PositionId}的职位信息"
                });
            }
        }

        var currentUserSubsidiaries = await GetUserOrganizationsAsync(user);

        var result =
            await RemoveFromUserSubsidiariesAsync(user, currentUserSubsidiaries.Except(userSubsidiaries).Distinct());
        if (!result.Succeeded)
        {
            return result;
        }

        result = await AddToUserSubsidiariesAsync(user, userSubsidiaries.Except(currentUserSubsidiaries).Distinct());
        if (!result.Succeeded)
        {
            return result;
        }

        return result;
    }


    public async Task<IdentityResult> SetRolesAsync(IdentityUser user, ICollection<string> roleNames)
    {
        Check.NotNull(user, nameof(user));
        Check.NotNull(roleNames, nameof(roleNames));
        var currentRoleNames = await GetRolesAsync(user);
        var result = await RemoveFromRolesAsync(user, currentRoleNames.Except(roleNames).Distinct());
        if (!result.Succeeded)
        {
            return result;
        }

        result = await AddToRolesAsync(user, roleNames.Except(currentRoleNames).Distinct());
        if (!result.Succeeded)
        {
            return result;
        }

        return IdentityResult.Success;
    }


    private async Task<IEnumerable<UserSubsidiary>> GetUserOrganizationsAsync(IdentityUser user)
    {
        var userSubsidiaries = UserSubsidiaryRepository.Where(p => p.UserId == user.Id);
        return await userSubsidiaries.ToListAsync();
    }

    private async Task<IdentityResult> AddToUserSubsidiariesAsync(IdentityUser user,
        IEnumerable<UserSubsidiary> userSubsidiaries)
    {
        foreach (var userSubsidiary in userSubsidiaries)
        {
            user.AddUserSubsidiaries(userSubsidiary.OrganizationId, userSubsidiary.PositionId);
        }

        return IdentityResult.Success;
    }

    private async Task<IdentityResult> RemoveFromUserSubsidiariesAsync(IdentityUser user,
        IEnumerable<UserSubsidiary> userSubsidiaries)
    {
        foreach (var userSubsidiary in userSubsidiaries)
        {
            user.RemoveUserSubsidiaries(userSubsidiary.OrganizationId, userSubsidiary.PositionId);
        }

        return IdentityResult.Success;
    }

    public Task<IdentityUser> FindByPhoneNumberAsync(string phoneNumber)
    {
        return ((IdentityUserStore)Store).FindByPhoneNumberAsync(phoneNumber);
    }

    public Task<IdentityUser> FindByAccountAsync(string account, long? tenantId, bool includeDetails = false)
    {
        ThrowIfDisposed();
        if (account == null)
        {
            throw new ArgumentNullException(nameof(account));
        }

        account = NormalizeName(account);
        return ((IdentityUserStore)Store).FindByAccountAsync(account, tenantId, includeDetails);
    }

    public override async Task<IdentityResult> RemovePasswordAsync(IdentityUser user)
    {
        ThrowIfDisposed();
        if (user == null)
            throw new ArgumentNullException(nameof(user));
        var identityResult = await UpdatePasswordHash(user, null, false);
        return identityResult;
    }


    public async Task<IdentityUser> GetByIdAsync(long userId, bool includeDetails)
    {
        if (!includeDetails)
        {
            return await GetByIdAsync(userId);
        }

        return await UserRepository.Include(p => p.Claims)
            .Include(p => p.Logins)
            .Include(p => p.Roles)
            .Include(p => p.Tokens)
            .Include(p => p.UserSubsidiaries)
            .FirstOrDefaultAsync(p => p.Id == userId);
    }

    public async Task<PagedList<GetUserPageOutput>> GetPageAsync(GetUserPageInput input)
    {
        var userPage = await UserRepository
            .Include(p=> p.UserSubsidiaries)
            .Where(!input.UserName.IsNullOrEmpty(), p => p.UserName.Contains(input.UserName))
            .Where(!input.Email.IsNullOrEmpty(), p => p.Email.Contains(input.Email))
            .Where(!input.MobilePhone.IsNullOrEmpty(), p => p.MobilePhone.Contains(input.MobilePhone))
            .Where(!input.TelPhone.IsNullOrEmpty(), p => p.TelPhone.Contains(input.TelPhone))
            .Where(!input.JobNumber.IsNullOrEmpty(), p => p.JobNumber.Contains(input.JobNumber))
            .Where(!input.RealName.IsNullOrEmpty(), p => p.RealName.Contains(input.RealName))
            .Where(input.Sex.HasValue, p => p.Sex == input.Sex)
            .Where(input.OrganizationIds != null && input.OrganizationIds.Any(),p=> p.UserSubsidiaries.Any(q=> input.OrganizationIds.Contains(q.OrganizationId)))
            .Where(input.PositionIds != null && input.PositionIds.Any(), p => p.UserSubsidiaries.Any(q => input.PositionIds.Contains(q.PositionId)))
            .ToPagedListAsync(input.PageIndex, input.PageSize);
        return userPage.Adapt<PagedList<GetUserPageOutput>>();
    }

    public async Task<PagedList<GetAddOrganizationUserOutput>> GetAddOrganizationUserPageAsync(long organizationId, GetAddOrganizationUserPageInput input)
    {
        var userPage = await UserRepository
            .Include(p => p.UserSubsidiaries)
            .Where(!input.UserName.IsNullOrEmpty(), p => p.UserName.Contains(input.UserName))
            .Where(!input.RealName.IsNullOrEmpty(), p => p.RealName.Contains(input.RealName))
            .ToPagedListAsync(input.PageIndex, input.PageSize);
        var userOutputList = new List<GetAddOrganizationUserOutput>();
        foreach (var user in userPage.Items) 
        {
            var userOutput = user.Adapt<GetAddOrganizationUserOutput>();
            var userOrganizationPosition = user.UserSubsidiaries.FirstOrDefault(p => p.OrganizationId == organizationId);
            if (userOrganizationPosition != null) 
            {
                userOutput.PositionId = userOrganizationPosition.PositionId;
                userOutput.PositionName = (await _positionAppService.GetAsync(userOrganizationPosition.PositionId)).Name;
            }
            userOutputList.Add(userOutput);
        }
        return new PagedList<GetAddOrganizationUserOutput>()
        {
            Items = userOutputList,
            HasNextPages = userPage.HasNextPages,
            HasPrevPages = userPage.HasPrevPages,
            TotalCount = userPage.TotalCount,
            TotalPages = userPage.TotalPages,
        };

    }

    public async Task<ICollection<GetUserOutput>> GetOrganizationUsersAsync(long organizationId)
    {
        var users = UserRepository.Include(p => p.UserSubsidiaries)
            .Where(p => p.UserSubsidiaries.Any(us => us.OrganizationId == organizationId));
        return (await users.ToListAsync()).Adapt<ICollection<GetUserOutput>>();
    }

    public async Task<bool> HasOrganizationUsersAsync(long organizationId)
    {
        var userCount = await UserRepository.Include(p => p.UserSubsidiaries)
            .CountAsync(p => p.UserSubsidiaries.Any(us => us.OrganizationId == organizationId));
        return userCount > 0;
    }

    public async Task<bool> HasPositionUsersAsync(long positionId)
    {
        var userCount = await UserRepository.Include(p => p.UserSubsidiaries)
            .CountAsync(p => p.UserSubsidiaries.Any(us => us.PositionId == positionId));
        return userCount > 0;
    }

    public async Task UpdateClaimTypesAsync(long userId, ICollection<UpdateClaimTypeInput> inputs)
    {
        if (inputs.Any(i => i.UserId != userId))
        {
            throw new UserFriendlyException($"指定的userId不相等");
        }

        var currentUserClaims = await UserClaimRepository
            .Where(p => p.UserId == userId)
            .AsNoTracking()
            .ToListAsync();

        var userClaims = new List<IdentityUserClaim>();
        foreach (var input in inputs)
        {
            var claimType = await ClaimTypeRepository.FirstOrDefaultAsync(p => p.Name == input.ClaimType);
            if (claimType == null)
            {
                throw new UserFriendlyException($"没有设置{input.ClaimType}的声明类型");
            }

            if (!claimType.Regex.IsNullOrEmpty() && !Regex.IsMatch(input.ClaimValue, claimType.Regex))
            {
                throw new UserFriendlyException($"设置的声明类型{input.ClaimType}的值{input.ClaimValue}格式不正确");
            }

            userClaims.Add(new IdentityUserClaim(input.UserId, input.ClaimType, input.ClaimValue));
        }

        await UserClaimRepository.DeleteAsync(currentUserClaims);
        await UserClaimRepository.InsertAsync(userClaims);
    }

    public async Task<UserDataRange> GetUserDataRange(long userId)
    {
        UserDataRange userDataRange;
        var userRoles = await UserRepository.GetRolesAsync(userId);
        if (userRoles.Any(p => p.DataRange == DataRange.Whole))
        {
            userDataRange = new UserDataRange(userId, true);
        }
        else
        {
            var userOrganizationIds = new List<long>();
            var roleDataRanges = new List<DataRange>();
            foreach (var role in userRoles)
            {
                if (roleDataRanges.Any(p => p == role.DataRange))
                {
                    continue;
                }

                switch (role.DataRange)
                {
                    case DataRange.CustomOrganization:
                        userOrganizationIds.AddRange(await GetRoleCustomDataRangeOrganizationIds(role.Id));
                        break;
                    case DataRange.SelfOrganization:
                        userOrganizationIds.AddRange(await GetSelfDataRangeOrganizationIds(userId));
                        break;
                    case DataRange.SelfAndChildrenOrganization:
                        userOrganizationIds.AddRange(await GetSelfAndChildrenDataRangeOrganizationIds(userId));
                        break;
                    default:
                        throw new BusinessException("role.DataRange is Wrong");
                }

                roleDataRanges.Add(role.DataRange);
            }

            userDataRange = new UserDataRange(userId, false, userOrganizationIds.Distinct().ToList());
        }

        return userDataRange;
    }

    private async Task<IEnumerable<long>> GetSelfAndChildrenDataRangeOrganizationIds(long userId)
    {
        var allSelfAndChildrenOrganizationIds = new List<long>();
        var user = await UserRepository.Include(p => p.UserSubsidiaries).AsNoTracking().FirstAsync(p => p.Id == userId);
        foreach (var userSubsidiary in user.UserSubsidiaries)
        {
            var selfAndChildrenOrganizationIds =
                await _organizationAppService.GetSelfAndChildrenOrganizationIdsAsync(userSubsidiary.OrganizationId);
            allSelfAndChildrenOrganizationIds.AddRange(selfAndChildrenOrganizationIds);
        }

        return allSelfAndChildrenOrganizationIds;
    }

    private async Task<IEnumerable<long>> GetSelfDataRangeOrganizationIds(long userId)
    {
        var user = await UserRepository.Include(p => p.UserSubsidiaries).AsNoTracking().FirstAsync(p => p.Id == userId);
        return user.UserSubsidiaries.Select(p => p.OrganizationId);
    }


    private async Task<IEnumerable<long>> GetRoleCustomDataRangeOrganizationIds(long roleId)
    {
        var roleCustomDataRangeOrganizations =
            await _roleOrganizationRepository.Where(p => p.RoleId == roleId).AsNoTracking().ToListAsync();
        return roleCustomDataRangeOrganizations.Select(p => p.OrganizationId);
    }
}