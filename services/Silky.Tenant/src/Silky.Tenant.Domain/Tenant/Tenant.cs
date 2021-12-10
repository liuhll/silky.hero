using System;
using Silky.EntityFrameworkCore.Entities;
using Silky.Hero.Common.EntityFrameworkCore.Entities;
using Silky.Hero.Common.Enums;

namespace Silky.Tenant.Domain;

public class Tenant : IEntity, ICreatedObject, IUpdatedObject, ISoftDeletedObject
{
    public Tenant()
    {
        Id = Guid.NewGuid();
    }

    public Tenant(Guid id, string name, Status status, string remark)
    {
        Id = id;
        Name = name;
        Status = status;
        Remark = remark;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public Status Status { get; set; }

    public string Remark { get; set; }
    public long? CreatedBy { get; set; }
    public DateTimeOffset CreatedTime { get; set; }

    public long? UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedTime { get; set; }
    public bool IsDeleted { get; set; }
    public long? DeletedBy { get; set; }
    public DateTimeOffset? DeletedTime { get; set; }
}