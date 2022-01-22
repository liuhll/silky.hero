using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Entities.Configures;

namespace Silky.Position.Domain;

public class PositionSeedData : IEntitySeedData<Position>
{
    public IEnumerable<Position> HasData(DbContext dbContext, Type dbContextLocator)
    {
        var initData = new List<Position>();
        initData.Add(new()
        {
            Id = 1,
            Name = "总经理",
            TenantId = 1,
            Sort = 99,
        });
        initData.Add(new()
        {
            Id = 2,
            Name = "技术总监",
            TenantId = 1,
            Sort = 98,
        });
        initData.Add(new()
        {
            Id = 3,
            Name = "后台组长",
            TenantId = 1,
            Sort = 97,
        });
        initData.Add(new()
        {
            Id = 4,
            Name = "前台组长",
            TenantId = 1,
            Sort = 96,
        });
        return initData;
    }
}