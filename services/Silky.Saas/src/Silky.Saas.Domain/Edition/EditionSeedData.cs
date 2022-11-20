using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Entities.Configures;

namespace Silky.Saas.Domain;

public class EditionSeedData : IEntitySeedData<Edition>
{
    public IEnumerable<Edition> HasData(DbContext dbContext, Type dbContextLocator)
    {
        var initData = new List<Edition>();
        initData.Add(new Edition()
        {
            Id = 1,
            Name = "标准版",
            Price = 0,
            Sort = 0,
        });
        initData.Add(new Edition()
        {
            Id = 2,
            Name = "专业版",
            Price = 500,
            Sort = 100,
        });
        initData.Add(new Edition()
        {
            Id = 3,
            Name = "旗舰版",
            Price = 2000,
            Sort = 1000
        });
        return initData;
    }
}