using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.Hero.Common.Enums;

namespace Silky.Saas.Domain;

public class TenantSendData : IEntitySeedData<Tenant>
{
    public IEnumerable<Tenant> HasData(DbContext dbContext, Type dbContextLocator)
    {
        var initList = new List<Tenant>();
        initList.Add(new(1,3, "Silky.Hero", "Silky开源社区", Status.Valid, "Silky.Hero权限管理系统开发社区"));
        initList.Add(new(2, 2,"Silky","Silky开源社区", Status.Valid, "Silky微服务开发框架"));
        return initList;
    }
}