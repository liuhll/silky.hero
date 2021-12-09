using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.Hero.Common.Enums;

namespace Silky.Tenant.Domain;

public class TenantSendData : IEntitySeedData<Tenant>
{
    public IEnumerable<Tenant> HasData(DbContext dbContext, Type dbContextLocator)
    {
        var initList = new List<Tenant>();
        initList.Add(new Tenant(Guid.NewGuid(), "Silky", Status.Valid, "silky微服务开发社区"));
        initList.Add(new Tenant(Guid.NewGuid(), "Hero", Status.Valid, "SilkyHero快速开发框架"));
        return initList;
    }
}