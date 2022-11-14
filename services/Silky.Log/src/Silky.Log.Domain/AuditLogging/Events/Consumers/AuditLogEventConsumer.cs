using System.Threading.Tasks;
using Mapster;
using MassTransit;
using Silky.Core;
using Silky.Core.Extensions;
using Silky.Core.Runtime.Session;
using Silky.EntityFrameworkCore.Repositories;
using Silky.MassTransit.Consumer;
using Silky.Rpc.Auditing;
using Silky.Rpc.Security;
using Silky.Saas.Application.Contracts.Edition;
using Silky.Saas.Domain.Shared.Feature;

namespace Silky.Log.Domain.AuditLogging.Events.Consumers;

public class AuditLogEventConsumer : SilkyConsumer<AuditLogInfo>
{
    private readonly IRepository<AuditLog> _auditLogRepository;
    private readonly IEditionAppService _editionAppService;
    private readonly ICurrentRpcToken _currentRpcToken;
    private static object locker = new();

    public AuditLogEventConsumer()
    {
        _auditLogRepository = EngineContext.Current.Resolve<IRepository<AuditLog>>();
        _editionAppService = EngineContext.Current.Resolve<IEditionAppService>();
        _currentRpcToken =  EngineContext.Current.Resolve<ICurrentRpcToken>();
    }


    protected override async Task ConsumeWork(ConsumeContext<AuditLogInfo> context)
    {
        if (context.Message.TenantId == null)
        {
            return;
        }
        var enabledAuditingLog = await _editionAppService
            .GetTenantEditionFeatureAsync(FeatureCode.EnabledAuditingLog,
                context.Message.TenantId.To<long>());
        if (enabledAuditingLog?.FeatureValue.To<bool>() == false)
        {
            return;
        }

        lock (locker)
        {
            var auditLog = context.Message.Adapt<AuditLog>();
            _auditLogRepository.InsertNow(auditLog);
        }
    }
}