using Silky.Identity.Domain.Shared;

namespace Silky.Identity.Application.Contracts.Role.Dtos;

public class CheckRoleInput
{
    public long? Id { get; set; }

    public string Name { get; set; }

    public RoleNameType RoleNameType { get; set; }
}