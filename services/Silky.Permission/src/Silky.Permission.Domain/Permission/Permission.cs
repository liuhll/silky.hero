using System;
using Silky.EntityFrameworkCore.Entities;
using Silky.Hero.Common.EntityFrameworkCore.Entities;

namespace Silky.Permission.Domain;

public class Permission : Entity<long>, ICreatedObject, IUpdatedObject, ISoftDeletedObject
{
    public string Code { get; set; }

    public string HostName { get; set; }

    public string ServiceName { get; set; }

    public string ServiceEntryId { get; set; }

    public string Method { get; set; }

    public string WebApi { get; set; }

    public string HttpMethod { get; set; }
    public long? CreatedBy { get; set; }
    public long? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public long? DeletedBy { get; set; }
    public DateTimeOffset? DeletedTime { get; set; }
}