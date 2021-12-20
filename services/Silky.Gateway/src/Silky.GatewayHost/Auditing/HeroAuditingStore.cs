using System.Threading.Tasks;
using Silky.Http.Auditing;
using Silky.Rpc.Auditing;

namespace Silky.GatewayHost.Auditing;

public class HeroAuditingStore : IAuditingStore
{
    public async Task SaveAsync(AuditLogInfo auditLogInfo)
    {
    }
}