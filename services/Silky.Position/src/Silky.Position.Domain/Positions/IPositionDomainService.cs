using System.Threading.Tasks;
using Silky.Core.DependencyInjection;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Position.Application.Contracts.Position.Dtos;

namespace Silky.Position.Domain.Positions;

public interface IPositionDomainService : IScopedDependency
{ 
    IRepository<Position> PositionRepository { get; }
    Task CreateAsync(CreateOrUpdatePositionInput input);
    Task UpdateAsync(CreateOrUpdatePositionInput input);
    Task DeleteAsync(long id);
}