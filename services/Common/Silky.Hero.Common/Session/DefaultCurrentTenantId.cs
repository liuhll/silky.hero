using Silky.Core.DependencyInjection;
using Silky.Core.Runtime.Session;

namespace Silky.Hero.Common.Session;

public class DefaultCurrentTenantId : ICurrentTenantId, IScopedDependency
{
    private long? _tenantId;
    private bool isSetTenantId = false;

    public long? TenantId
    {
        get
        {
            if (isSetTenantId)
            {
                return _tenantId;
            }

            if (NullSession.Instance.TenantId != null)
            {
                return long.Parse(NullSession.Instance.TenantId.ToString());
            }

            return null;
        }
    }

    public void SetTenantId(long? tenantId)
    {
        isSetTenantId = true;
        _tenantId = tenantId;
    }
}