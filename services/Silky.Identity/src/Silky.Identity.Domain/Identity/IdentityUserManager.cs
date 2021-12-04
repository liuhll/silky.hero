using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Silky.Core;
using Silky.Core.Extensions;
using Silky.EntityFrameworkCore.Extensions;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Hero.Common.EntityFrameworkCore;
using Silky.Identity.Application.Contracts.User.Dtos;

namespace Silky.Identity.Domain;

public class IdentityUserManager : UserManager<IdentityUser>
{
    protected IRepository<UserOrganization> UserOrganizationRepository { get; }

    protected IIdentityUserRepository UserRepository { get; }

    public IdentityUserManager(IdentityUserStore store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<IdentityUser> passwordHasher,
        IEnumerable<IUserValidator<IdentityUser>> userValidators,
        IEnumerable<IPasswordValidator<IdentityUser>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<IdentityUserManager> logger,
        IRepository<UserOrganization> userOrganizationRepository,
        IIdentityUserRepository userRepository)
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
        UserOrganizationRepository = userOrganizationRepository;
        UserRepository = userRepository;
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
        ICollection<UserOrganization> userOrganizations)
    {
        Check.NotNull(user, nameof(user));
        Check.NotNull(userOrganizations, nameof(userOrganizations));

        var currentUserSubsidiaries = await GetUserOrganizationsAsync(user);

        var result =
            await RemoveFromUserOrganizationsAsync(user, currentUserSubsidiaries.Except(userOrganizations).Distinct());
        if (!result.Succeeded)
        {
            return result;
        }

        result = await AddToUserOrganizationsAsync(user, userOrganizations.Except(currentUserSubsidiaries).Distinct());
        if (!result.Succeeded)
        {
            return result;
        }

        return result;
    }

    private async Task<IEnumerable<UserOrganization>> GetUserOrganizationsAsync(IdentityUser user)
    {
        var userSubsidiaries = UserOrganizationRepository.Where(p => p.UserId == user.Id);
        return await userSubsidiaries.ToListAsync();
    }

    private async Task<IdentityResult> AddToUserOrganizationsAsync(IdentityUser user,
        IEnumerable<UserOrganization> userOrganizations)
    {
        foreach (var userSubsidiary in userOrganizations)
        {
            await UserOrganizationRepository.InsertAsync(userSubsidiary);
        }

        return IdentityResult.Success;
    }

    private async Task<IdentityResult> RemoveFromUserOrganizationsAsync(IdentityUser user,
        IEnumerable<UserOrganization> userOrganizations)
    {
        foreach (var userSubsidiary in userOrganizations)
        {
            await UserOrganizationRepository.DeleteAsync(userSubsidiary);
        }

        return IdentityResult.Success;
    }

    public Task<IdentityUser> FindByPhoneNumberAsync(string phoneNumber)
    {
        return ((IdentityUserStore)Store).FindByPhoneNumberAsync(phoneNumber);
    }

    public Task<IdentityUser> FindByAccountAsync(string account, bool includeDetails = false)
    {
        ThrowIfDisposed();
        if (account == null)
        {
            throw new ArgumentNullException(nameof(account));
        }

        account = NormalizeName(account);
        return ((IdentityUserStore)Store).FindByAccountAsync(account, includeDetails);
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
            .Include(p => p.UserOrganizations)
            .FirstOrDefaultAsync(p => p.Id == userId);
    }

    public async Task<PagedList<GetUserPageOutput>> GetPageAsync(GetUserPageInput input)
    {
        var userPage = await UserRepository
            .Where(!input.UserName.IsNullOrEmpty(), p => p.UserName.Contains(input.UserName))
            .Where(!input.Email.IsNullOrEmpty(), p => p.Email.Contains(input.Email))
            .Where(!input.MobilePhone.IsNullOrEmpty(), p => p.MobilePhone.Contains(input.MobilePhone))
            .Where(!input.TelPhone.IsNullOrEmpty(), p => p.TelPhone.Contains(input.TelPhone))
            .Where(!input.JobNumber.IsNullOrEmpty(), p => p.JobNumber.Contains(input.JobNumber))
            .Where(!input.RealName.IsNullOrEmpty(), p => p.RealName.Contains(input.RealName))
            .Where(input.Sex.HasValue, p => p.Sex == input.Sex)
            .ToPagedListAsync(input.PageIndex, input.PageSize);
        return userPage.Adapt<PagedList<GetUserPageOutput>>();
    }

    public async Task<ICollection<GetUserOutput>> GetOrganizationUsersAsync(long organizationId)
    {
        var users = UserRepository.Include(p => p.UserOrganizations)
            .Where(p => p.UserOrganizations.Any(us => us.OrganizationId == organizationId));
        return (await users.ToListAsync()).Adapt<ICollection<GetUserOutput>>();
    }

    public async Task<bool> HasOrganizationUsersAsync(long organizationId)
    {
        var userCount = await UserRepository.Include(p => p.UserOrganizations)
            .CountAsync(p => p.UserOrganizations.Any(us => us.OrganizationId == organizationId));
        return userCount > 0;
    }

    public async Task<bool> HasPositionUsersAsync(long positionId)
    {
        var userCount = await UserRepository.Include(p => p.UserOrganizations)
            .CountAsync(p => p.UserOrganizations.Any(us => us.PositionId == positionId));
        return userCount > 0;
    }
}