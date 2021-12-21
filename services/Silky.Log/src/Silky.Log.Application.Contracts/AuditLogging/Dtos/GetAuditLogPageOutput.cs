using System;

namespace Silky.Log.Application.Contracts.AuditLogging.Dtos;

public class GetAuditLogPageOutput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 执行时间
    /// </summary>
    public DateTimeOffset ExecutionTime { get; set; }

    /// <summary>
    /// 执行时长(ms)
    /// </summary>
    public int ExecutionDuration { get; set; }

    /// <summary>
    /// 请求的URL API地址
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 请求的http方法
    /// </summary>
    public string HttpMethod { get; set; }

    /// <summary>
    /// 客户端地址
    /// </summary>
    public string ClientIpAddress { get; set; }

    /// <summary>
    /// 响应码
    /// </summary>
    public int? HttpStatusCode { get; set; }

}