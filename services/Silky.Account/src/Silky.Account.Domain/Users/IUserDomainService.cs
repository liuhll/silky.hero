using System.Threading.Tasks;
using Silky.Account.Application.Contracts.User.Dtos;

namespace Silky.Account.Domain.Users;

public interface IUserDomainService
{
    Task Create(CreateUserInput input);
}