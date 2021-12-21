using System;
using Silky.Hero.Common.Dtos;

namespace Silky.Log.Application.Contracts.AuditLogging.Dtos;

public class GetAuditLogPageInput : PageDtoBase
{
    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTimeOffset? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTimeOffset? EndTime { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// URL地址
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 最大持续时间(ms)
    /// </summary>
    public int? MaxExecutionDuration { get; set; }

    /// <summary>
    /// 最小持续时间
    /// </summary>
    public int? MinExecutionDuration { get; set; }

    /// <summary>
    /// http请求方法
    /// </summary>
    public string HttpMethod { get; set; }
    
    
    /// <summary>
    /// 响应码
    /// </summary>
    public int? HttpStatusCode { get; set; }

    /// <summary>
    /// 是否存在异常
    /// </summary>
    public bool? HasException { get; set; }
}