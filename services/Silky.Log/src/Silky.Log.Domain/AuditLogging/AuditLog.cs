using System;
using System.Collections.Generic;
using Silky.EntityFrameworkCore.Entities;
using Silky.EntityFrameworkCore.Extras.Entities;
using Silky.Hero.Common.EntityFrameworkCore.Entities;

namespace Silky.Log.Domain.AuditLogging;

public class AuditLog : IEntity, IHasTenantObject
{
    public long Id { get; set; }
    public long? UserId { get; set; }

    public string UserName { get; set; }

    public long? TenantId { get; set; }

    public DateTimeOffset ExecutionTime { get; set; }

    public int ExecutionDuration { get; set; }

    public string BrowserInfo { get; set; }

    public string Url { get; set; }

    public string HttpMethod { get; set; }

    public string ClientIpAddress { get; set; }

    public string ClientId { get; set; }

    public string CorrelationId { get; set; }

    public int? HttpStatusCode { get; set; }

    public string ExceptionMessage { get; set; }
    
    public string RequestParameters { get; set; }

    public virtual ICollection<AuditLogAction> Actions { get; set; }
    
}