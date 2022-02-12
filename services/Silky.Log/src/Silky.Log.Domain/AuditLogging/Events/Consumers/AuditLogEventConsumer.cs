using System.Threading.Tasks;
using Mapster;
using MassTransit;
using Silky.Core;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Rpc.Auditing;

namespace Silky.Log.Domain.AuditLogging.Events.Consumers;

public class AuditLogEventConsumer: IConsumer<AuditLogInfo>
{
    private readonly IRepository<AuditLog> _auditLogRepository;

    public AuditLogEventConsumer()
    {
        _auditLogRepository = EngineContext.Current.Resolve<IRepository<AuditLog>>();
    }

    public async Task Consume(ConsumeContext<AuditLogInfo> context)
    {
        var auditLog = context.Message.Adapt<AuditLog>();
        await _auditLogRepository.InsertNowAsync(auditLog);
    }
}