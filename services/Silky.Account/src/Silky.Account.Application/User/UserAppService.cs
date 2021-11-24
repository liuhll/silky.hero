using System.Threading.Tasks;
using Silky.Account.Application.Contracts.User;
using Silky.Account.Application.Contracts.User.Dtos;
using Silky.Account.Domain.Users;

namespace Silky.Account.Application.User;

public class UserAppService : IUserAppService
{
    private readonly IUserDomainService _userDomainService;

    public UserAppService(IUserDomainService userDomainService)
    {
        _userDomainService = userDomainService;
    }

    public Task Create(CreateUserInput input)
    {
        return _userDomainService.Create(input);
    }
}