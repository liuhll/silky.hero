using System.Collections.Generic;
using Silky.Log.Domain.Shared.AuditLogging;

namespace Silky.Log.Application.Contracts.AuditLogging.Dtos;

public class GetAuditLogOutput : AuditLogDtoBase
{
    /// <summary>
    /// 浏览器信息
    /// </summary>
    public string BrowserInfo { get; set; }

    /// <summary>
    /// 客户id
    /// </summary>

    public string ClientId { get; set; }
    
    /// <summary>
    /// 异常信息
    /// </summary>
    public string ExceptionMessage { get; set; }

    /// <summary>
    /// 执行的方法
    /// </summary>
    public virtual ICollection<GetAuditLogActionOutput> Actions { get; set; }
}