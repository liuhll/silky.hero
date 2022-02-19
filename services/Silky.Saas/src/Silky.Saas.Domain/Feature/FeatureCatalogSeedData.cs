using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Entities.Configures;

namespace Silky.Saas.Domain;

public class FeatureCatalogSeedData : IEntitySeedData<FeatureCatalog>
{
    public IEnumerable<FeatureCatalog> HasData(DbContext dbContext, Type dbContextLocator)
    {
        var initData = new List<FeatureCatalog>();
        initData.Add(new FeatureCatalog()
        {
            Id = 1,
            Name = "身份标识"
        });
        initData.Add(new FeatureCatalog()
        {
            Id = 2,
            Name = "审计日志"
        });
        
        initData.Add(new FeatureCatalog()
        {
            Id = 3,
            Name = "权限管理"
        });
        
        initData.Add(new FeatureCatalog()
        {
            Id = 4,
            Name = "Saas"
        });
        return initData;
    }
}