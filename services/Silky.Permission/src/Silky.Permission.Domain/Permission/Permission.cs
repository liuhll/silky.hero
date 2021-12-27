using Silky.Hero.Common.EntityFrameworkCore.Entities;

namespace Silky.Permission.Domain;

public class Permission : FullAuditedEntity
{
    public string Code { get; set; }

    public string HostName { get; set; }

    public string ServiceName { get; set; }

    public string ServiceEntryId { get; set; }

    public string Method { get; set; }

    public string WebApi { get; set; }

    public string HttpMethod { get; set; }
}