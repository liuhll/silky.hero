﻿using System.Threading;
using System.Threading.Tasks;
using Mapster;
using MassTransit;
using Silky.Core;
using Silky.Core.Extensions;
using Silky.Core.Runtime.Session;
using Silky.Core.Threading;
using Silky.EntityFrameworkCore.Repositories;
using Silky.MassTransit.Consumer;
using Silky.Rpc.Auditing;
using Silky.Saas.Application.Contracts.Edition;
using Silky.Saas.Domain.Shared.Feature;

namespace Silky.Log.Domain.AuditLogging.Events.Consumers;

public class AuditLogEventConsumer : SilkyConsumer<AuditLogInfo>
{
    private readonly IRepository<AuditLog> _auditLogRepository;
    private readonly IEditionAppService _editionAppService;
    private static SemaphoreSlim _syncSemaphore = new SemaphoreSlim(1, 1);


    public AuditLogEventConsumer()
    {
        _auditLogRepository = EngineContext.Current.Resolve<IRepository<AuditLog>>();
        _editionAppService = EngineContext.Current.Resolve<IEditionAppService>();
    }
    

    protected override async Task ConsumeWork(ConsumeContext<AuditLogInfo> context)
    {
        var session = NullSession.Instance;
        if (session.TenantId != null)
        {
            var enabledAuditingLog = await _editionAppService
                .GetTenantEditionFeatureAsync(FeatureCode.EnabledAuditingLog, session.TenantId.To<long>());

            if (enabledAuditingLog?.FeatureValue.To<bool>() == false)
            {
                return;
            }
        }
        
        using (await _syncSemaphore.LockAsync())
        {
            var message = context.Message;
            var auditLog = message.Adapt<AuditLog>();
            await _auditLogRepository.InsertNowAsync(auditLog);
        }

    }
}