namespace Silky.Saas.Application.Contracts.Tenant.Dtos;

public class UpdateTenantInput : TenantDtoBase
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }
}