using Silky.Core.DependencyInjection;
using Silky.Core.Runtime.Session;

namespace Silky.Hero.Common.Session;

public class DefaultCurrentTenantId : ICurrentTenantId, IScopedDependency
{
    private long? _tenantId;

    public long? TenantId
    {
        get
        {
            if (_tenantId != null)
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
        _tenantId = tenantId;
    }
}