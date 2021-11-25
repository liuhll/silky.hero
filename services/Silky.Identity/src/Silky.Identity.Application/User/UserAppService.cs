using System.Threading.Tasks;
using Silky.Core.Exceptions;
using Silky.Identity.Application.Contracts.User;
using Silky.Identity.Application.Contracts.User.Dtos;
using Silky.Identity.Domain;

namespace Silky.Identity.Application.User;

public class UserAppService : IUserAppService
{
    private readonly IdentityUserManager _userManager;

    public UserAppService(IdentityUserManager userManager)
    {
        _userManager = userManager;
    }

    public async Task CreateOrUpdate(CreateOrUpdateUserInput input)
    {
        var user = new IdentityUser(input.UserName,input.Email)
        {
            Sex = input.Sex,
            BirthDay = input.BirthDay,
            JobNumber = input.JobNumber,
            RealName = input.RealName,
            Surname = input.Surname,
            TelPhone = input.TelPhone,
            MobilePhone = input.MobilePhone,
            PositionId = input.PositionId,
            OrganizationId = input.OrganizationId,
        };
        var identityResult = await _userManager.CreateAsync(user);
        if (!identityResult.Succeeded)
        {
            throw new UserFriendlyException("新增用户信息失败!");
        }
    }
}