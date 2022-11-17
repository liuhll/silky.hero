using Mapster;
using Silky.Rpc.Auditing;

namespace Silky.Log.Domain.AuditLogging.Events.Consumers;

public class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<AuditLogInfo, AuditLog>()
            .Map(dest => dest.TenantId, src => src.TenantId != null ? src.TenantId.ToString() : null)
            .Map(dest => dest.UserId, src => src.UserId != null ? src.UserId.ToString() : null)
            ;
    }
}