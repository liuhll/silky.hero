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
    private static object locker = new();

    public AuditLogEventConsumer()
    {
        _auditLogRepository = EngineContext.Current.Resolve<IRepository<AuditLog>>();
    }

    public async Task Consume(ConsumeContext<AuditLogInfo> context)
    {
        lock (locker)
        {
            var auditLog = context.Message.Adapt<AuditLog>();
             _auditLogRepository.InsertNow(auditLog);
        }
        
    }
}