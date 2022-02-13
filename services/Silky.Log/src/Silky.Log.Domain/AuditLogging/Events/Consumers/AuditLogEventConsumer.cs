using System.Threading.Tasks;
using Mapster;
using MassTransit;
using Silky.Core;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Rpc.Auditing;

namespace Silky.Log.Domain.AuditLogging.Events.Consumers;

public class AuditLogEventConsumer: IConsumer<AuditLogInfo>
{
    
    public async Task Consume(ConsumeContext<AuditLogInfo> context)
    {
        var auditLogRepository = EngineContext.Current.Resolve<IRepository<AuditLog>>();
        var auditLog = context.Message.Adapt<AuditLog>();
        await auditLogRepository.InsertNowAsync(auditLog);
    }
}