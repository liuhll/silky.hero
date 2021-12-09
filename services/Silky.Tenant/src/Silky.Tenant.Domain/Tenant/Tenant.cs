using System;
using Silky.EntityFrameworkCore.Entities;
using Silky.Hero.Common.EntityFrameworkCore.Entities;
using Silky.Hero.Common.Enums;

namespace Silky.Tenant.Domain;

public class Tenant : Entity<Guid>, IAuditedObject
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

    public string Name { get; set; }

    public Status Status { get; set; }

    public string Remark { get; set; }
    public long? CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }
}