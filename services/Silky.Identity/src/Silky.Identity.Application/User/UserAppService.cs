using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Silky.Core.DbContext.UnitOfWork;
using Silky.Core.Extensions;
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

    public UserAppService(IdentityUserManager userManager)
    {
        UserManager = userManager;
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
        user.BirthDay = user.BirthDay;
        user.TelPhone = input.TelPhone;
        user.OrganizationId = input.OrganizationId;
        user.PositionId = input.PositionId;

        if (input.UserSubsidiaries != null)
        {
            var userSubsidiaries = input.UserSubsidiaries
                .Select(us => new UserSubsidiary(user.Id, us.OrganizationId, us.PositionId, user.TenantId)).ToList();
            (await UserManager.SetUserSubsidiaries(user, userSubsidiaries)).CheckErrors();
        }
    }
}