using Silky.Saas.Domain.Shared.Tenant;

namespace Silky.Saas.Application.Contracts.Tenant.Dtos;

public class CheckTenantInput
{
    public long? Id { get; set; }

    public string Name { get; set; }

    public TenantNameType NameType { get; set; }
}