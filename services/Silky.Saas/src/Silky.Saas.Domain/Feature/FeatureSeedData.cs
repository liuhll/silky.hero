using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.Saas.Domain.Shared.Feature;

namespace Silky.Saas.Domain;

public class FeatureSeedData : IEntitySeedData<Feature>
{
    public IEnumerable<Feature> HasData(DbContext dbContext, Type dbContextLocator)
    {
        var initData = new List<Feature>();
        initData.Add(new Feature()
        {
            Id = 1,
            FeatureCatalogId = 1,
            Name = "最大用户数",
            Description = "0 = 无限制",
            DefaultValue = 0,
            FeatureType = FeatureType.Number,
            Code = FeatureCode.AllowMaxUserCount,
        });
        
        initData.Add(new Feature()
        {
            Id = 2,
            FeatureCatalogId = 2,
            Name = "启用审计日志",
            Description = "在应用程序中启用审计日志页面.",
            DefaultValue = 0,
            FeatureType = FeatureType.Boolen,
            Code = FeatureCode.EnabledAuditingLog,
        });
        
        initData.Add(new Feature()
        {
            Id = 3,
            FeatureCatalogId = 3,
            Name = "启用菜单管理",
            Description = "在应用程序中启动菜单管理.",
            DefaultValue = 0,
            FeatureType = FeatureType.Boolen,
            Code = FeatureCode.EnabledMenuManage,
        });
        
        initData.Add(new Feature()
        {
            Id = 4,
            FeatureCatalogId = 3,
            Name = "启用微服务管理端",
            Description = "允许授权用户登录微服务管理端.",
            DefaultValue = 0,
            FeatureType = FeatureType.Boolen,
            Code = FeatureCode.EnabledSilkyDashboard,
        });
        
        initData.Add(new Feature()
        {
            Id = 5,
            FeatureCatalogId = 4,
            Name = "启用Saas管理",
            Description = "在应用程序中启动Saas管理.",
            DefaultValue = 0,
            FeatureType = FeatureType.Boolen,
            Code = FeatureCode.EnabledSaasManage,
        });
        return initData;
    }
}