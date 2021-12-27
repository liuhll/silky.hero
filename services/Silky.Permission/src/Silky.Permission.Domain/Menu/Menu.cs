﻿using Silky.Hero.Common.EntityFrameworkCore.Entities;
using Silky.Hero.Common.Enums;
using Silky.Permission.Domain.Shared.Menu;

namespace Silky.Permission.Domain.Menu;

public class Menu : FullAuditedEntity
{
    public string Name { get; set; }

    public string PermissionCode { get; set; }

    public long? ParentId { get; set; }

    public string Icon { get; set; }

    public int Sort { get; set; }

    public string RoutePath { get; set; }

    public string Component { get; set; }

    public Status Status { get; set; }

    public bool? ExternalLink { get; set; }

    public bool? Display { get; set; }

    public bool? Cache { get; set; }

    public MenuType Type { get; set; }
}