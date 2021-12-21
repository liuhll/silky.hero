using System.Threading.Tasks;
using Silky.Rpc.Auditing;
using Silky.Rpc.Routing;
using Silky.Rpc.Runtime.Server;

namespace Silky.Log.Application.Contracts.AuditLogging;

[ServiceRoute]
public interface IAuditLogAppService
{
    [Governance(ProhibitExtranet = true)]
    Task SaveAsync(AuditLogInfo auditLogInfo);
}