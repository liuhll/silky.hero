using System.Threading.Tasks;
using Mapster;
using MassTransit;
using Silky.Core;
using Silky.Core.Extensions;
using Silky.Core.Runtime.Session;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Rpc.Auditing;
using Silky.Rpc.Security;
using Silky.Saas.Application.Contracts.Edition;
using Silky.Saas.Domain.Shared.Feature;

namespace Silky.Log.Domain.AuditLogging.Events.Consumers;

public class AuditLogEventConsumer : IConsumer<AuditLogInfo>
{
    private readonly IRepository<AuditLog> _auditLogRepository;
    private readonly IEditionAppService _editionAppService;
    private readonly ISession _session;
    private static object locker = new();

    public AuditLogEventConsumer()
    {
        _auditLogRepository = EngineContext.Current.Resolve<IRepository<AuditLog>>();
        _editionAppService = EngineContext.Current.Resolve<IEditionAppService>();
        _session = NullSession.Instance;
    }

    public async Task Consume(ConsumeContext<AuditLogInfo> context)
    {
        lock (locker)
        {
            if (!_session.IsLogin())
            {
                return;
            }

            var enabledAuditingLog = _editionAppService.GetEditionFeatureAsync(FeatureCode.EnabledAuditingLog).GetAwaiter().GetResult();
            if (enabledAuditingLog?.FeatureValue.To<bool>() == false)
            {
                return;
            }
            var auditLog = context.Message.Adapt<AuditLog>();
            _auditLogRepository.InsertNow(auditLog);
        }
    }
}