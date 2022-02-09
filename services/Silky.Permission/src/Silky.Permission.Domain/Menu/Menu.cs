using System;
using System.Collections.Generic;
using Silky.EntityFrameworkCore.Entities;
using Silky.Hero.Common.EntityFrameworkCore.Entities;
using Silky.Hero.Common.Enums;
using Silky.Permission.Domain.Shared.Menu;

namespace Silky.Permission.Domain.Menu;

public class Menu : Entity<long>, ICreatedObject, IUpdatedObject, ISoftDeletedObject
{
    public string Name { get; set; }

    public string PermissionCode { get; set; }

    public long? ParentId { get; set; }

    public string Icon { get; set; }

    public int Sort { get; set; }

    public string RoutePath { get; set; }

    public string Component { get; set; }
    
    public bool? ExternalLink { get; set; }

    public bool? Display { get; set; }

    public bool? KeepAlive { get; set; }

    public bool? HideBreadcrumb { get; set; }

    public string CurrentActiveMenu { get; set; }

    public MenuType Type { get; set; }

    public Status Status { get; set; }

    public long? CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public long? DeletedBy { get; set; }
    public DateTimeOffset? DeletedTime { get; set; }

    public virtual ICollection<Menu> Children { get; set; }

    public virtual Menu Parent { get; set; }
}