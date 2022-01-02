namespace Silky.Hero.Common.Session;

public interface ICurrentTenantId
{
    long? TenantId { get; }

    void SetTenantId(long? tenantId);
}