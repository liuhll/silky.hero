using System.Threading.Tasks;
using Mapster;
using Silky.Account.Application.Contracts.User.Dtos;
using Silky.Core.DependencyInjection;
using Silky.EntityFrameworkCore.Repositories;

namespace Silky.Account.Domain.Users;

public class UserDomainService : IUserDomainService, ITransientDependency
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<UserSubsidiary> _userSubsidiaryRepository;

    public UserDomainService(IRepository<User> userRepository,
        IRepository<UserSubsidiary> userSubsidiaryRepository)
    {
        _userRepository = userRepository;
        _userSubsidiaryRepository = userSubsidiaryRepository;
    }

    public async Task Create(CreateUserInput input)
    {
        var user = input.Adapt<User>();
        // var userSubsidiaries = input.UserSubsidiaries.Adapt<ICollection<UserSubsidiary>>();
        // var userEntry = await _userRepository.InsertAsync(user);
        // foreach (var userSubsidiary in userSubsidiaries)
        // {
        //     userSubsidiary.UserId = userEntry.Entity.Id;
        // }
        await _userRepository.InsertAsync(user);
    }
}