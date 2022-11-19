using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Silky.BasicData.Domain.Shared;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.Identity.Domain.Shared;
using Silky.Log.Domain.Shared;
using Silky.Organization.Domain.Shared;
using Silky.Permission.Domain.Shared;
using Silky.Permission.Domain.Shared.Menu;
using Silky.Position.Domain.Shared;
using Silky.Saas.Domain.Shared;

namespace Silky.Permission.Domain.Menu;

public class MenuSeedData : IEntitySeedData<Menu>
{
    public IEnumerable<Menu> HasData(DbContext dbContext, Type dbContextLocator)
    {
        var initList = new List<Menu>();
        initList.Add(new Menu()
        {
            Id = 1,
            Name = "权限管理",
            Icon = "ion:key-outline",
            Type = MenuType.Catalog,
            ParentId = null,
            RoutePath = "/authorization",
            Component = "LAYOUT",
            ExternalLink = false,
            Display = true,
            Sort = 1000,
        });
        CreateUserMenu(initList);

        CreateOrganizationMenu(initList);

        CreatePositionMenu(initList);

        CreateMenuMenu(initList);

        CreateRoleMenu(initList);

        CreateDashboardMenu(initList);

        CreateLogMenu(initList);

        CreateAboutMenu(initList);
        
        CreateSaasMenu(initList);
        
        CreateBasicDataMenu(initList);
        
        CreateExternalLinkMenu(initList);

        return initList;
    }

    private void CreateExternalLinkMenu(List<Menu> initList)
    {
        initList.Add(new Menu()
        {
            Id = 10000,
            Name = "外部链接",
            ParentId = null,
            RoutePath = "/externallink",
            Component = "LAYOUT",
            ExternalLink = true,
            Display = true,
            Icon = "ion:tv-outline",
            Type = MenuType.Catalog,
            Sort = 0,
        });
        initList.Add(new Menu()
        {
            Id = 10001,
            Name = "Github地址",
            ParentId = 10000,
            RoutePath = "https://github.com/liuhll/silky.hero",
            ExternalLink = true,
            Display = true,
            Type = MenuType.Menu,
            Sort = 996,
        });
        initList.Add(new Menu()
        {
            Id = 10002,
            Name = "Swagger文档",
            ParentId = 10000,
            RoutePath = "https://localhost:5001/index.html",
            ExternalLink = true,
            Display = true,
            Type = MenuType.Menu,
            Sort = 995,
        });
        initList.Add(new Menu()
        {
            Id = 10003,
            Name = "服务健康检查",
            ParentId = 10000,
            RoutePath = "https://localhost:5001/healthchecks-ui",
            ExternalLink = true,
            Display = true,
            Type = MenuType.Menu,
            Sort = 994,
        });
        initList.Add(new Menu()
        {
            Id = 10004,
            Name = "微服务管理端",
            ParentId = 10000,
            RoutePath = "https://localhost:5001/dashboard/index.html",
            ExternalLink = true,
            Display = true,
            Type = MenuType.Menu,
            Sort = 993,
        });
    }

    private void CreateBasicDataMenu(List<Menu> initList)
    {
        initList.Add(new Menu()
        {
            Id = 3000,
            Name = "基础配置",
            ParentId = null,
            RoutePath = "/basic-config",
            Component = "LAYOUT",
            ExternalLink = false,
            Icon = "icon-park-outline:setting-config",
            Type = MenuType.Catalog,
            Sort = 996,
        });
        initList.Add(new Menu()
        {
            Id = 3001,
            Name = "字典管理",
            ParentId = 3000,
            RoutePath = "/basic-config/dictionary",
            Component = "/basic-config/dictionary/index",
            ExternalLink = false,
            Type = MenuType.Menu,
            Sort = 1000,
        });
        initList.Add(new Menu()
        {
            Id = 3002,
            Name = "字典项",
            ParentId = 3001,
            PermissionCode = BasicDataPermissions.Dictionaries.Items.Default,
            Type = MenuType.Button,
            Sort = 999,
        });
        initList.Add(new Menu()
        {
            Id = 3003,
            Name = "新增",
            ParentId = 3001,
            PermissionCode = BasicDataPermissions.Dictionaries.Types.Create,
            Type = MenuType.Button,
            Sort = 997,
        });
        initList.Add(new Menu()
        {
            Id = 3004,
            Name = "编辑",
            ParentId = 3001,
            PermissionCode = BasicDataPermissions.Dictionaries.Types.Update,
            Type = MenuType.Button,
            Sort = 996,
        });
        initList.Add(new Menu()
        {
            Id = 3005,
            Name = "删除",
            ParentId = 3001,
            PermissionCode = BasicDataPermissions.Dictionaries.Types.Delete,
            Type = MenuType.Button,
            Sort = 995,
        });
        
        initList.Add(new Menu()
        {
            Id = 3006,
            Name = "新增",
            ParentId = 3002,
            PermissionCode = BasicDataPermissions.Dictionaries.Items.Create,
            Type = MenuType.Button,
            Sort = 994,
        });
        initList.Add(new Menu()
        {
            Id = 3007,
            Name = "编辑",
            ParentId = 3002,
            PermissionCode = BasicDataPermissions.Dictionaries.Items.Update,
            Type = MenuType.Button,
            Sort = 993,
        });
        initList.Add(new Menu()
        {
            Id = 3008,
            Name = "删除",
            ParentId = 3002,
            PermissionCode = BasicDataPermissions.Dictionaries.Items.Delete,
            Type = MenuType.Button,
            Sort = 992,
        });
    }

    private void CreateSaasMenu(List<Menu> initList)
    {
        initList.Add(new Menu()
        {
            Id = 2400,
            Name = "Saas",
            ParentId = null,
            RoutePath = "/about",
            Component = "LAYOUT",
            ExternalLink = false,
            Icon = "ant-design:team-outlined",
            Type = MenuType.Catalog,
            Sort = 997,
        });
        initList.Add(new Menu()
        {
            Id = 2401,
            Name = "租户",
            ParentId = 2400,
            RoutePath = "/saas/tenant",
            Component = "/saas/tenant/index",
            ExternalLink = false,
            Type = MenuType.Menu,
            Sort = 997,
        });
        initList.Add(new Menu()
        {
            Id = 2402,
            Name = "新增",
            ParentId = 2401,
            PermissionCode = SaasPermissions.Tenants.Create,
            Type = MenuType.Button,
            Sort = 996,
        });
        initList.Add(new Menu()
        {
            Id = 2403,
            Name = "新增",
            ParentId = 2401,
            PermissionCode = SaasPermissions.Tenants.Create,
            Type = MenuType.Button,
            Sort = 995,
        });
        initList.Add(new Menu()
        {
            Id = 2404,
            Name = "编辑",
            ParentId = 2401,
            PermissionCode = SaasPermissions.Tenants.Update,
            Type = MenuType.Button,
            Sort = 994,
        });
        initList.Add(new Menu()
        {
            Id = 2405,
            Name = "删除",
            ParentId = 2401,
            PermissionCode = SaasPermissions.Tenants.Delete,
            Type = MenuType.Button,
            Sort = 993,
        });
        initList.Add(new Menu()
        {
            Id = 2406,
            Name = "查询",
            ParentId = 2401,
            PermissionCode = SaasPermissions.Tenants.Search,
            Type = MenuType.Button,
            Sort = 992,
        });
        initList.Add(new Menu()
        {
            Id = 2407,
            Name = "详情",
            ParentId = 2401,
            PermissionCode = SaasPermissions.Tenants.LookDetail,
            Type = MenuType.Button,
            Sort = 991,
        });
        
        initList.Add(new Menu()
        {
            Id = 2501,
            Name = "版本",
            ParentId = 2400,
            RoutePath = "/saas/edition",
            Component = "/saas/edition/index",
            ExternalLink = false,
            Type = MenuType.Menu,
            Sort = 990,
        });
        
        initList.Add(new Menu()
        {
            Id = 2502,
            Name = "新增",
            ParentId = 2501,
            PermissionCode = SaasPermissions.Editions.Create,
            Type = MenuType.Button,
            Sort = 989,
        });
        initList.Add(new Menu()
        {
            Id = 2503,
            Name = "编辑",
            ParentId = 2501,
            PermissionCode = SaasPermissions.Editions.Update,
            Type = MenuType.Button,
            Sort = 988,
        });
        initList.Add(new Menu()
        {
            Id = 2504,
            Name = "删除",
            ParentId = 2501,
            PermissionCode = SaasPermissions.Editions.Delete,
            Type = MenuType.Button,
            Sort = 987,
        });
        initList.Add(new Menu()
        {
            Id = 2505,
            Name = "设置功能",
            ParentId = 2501,
            PermissionCode = SaasPermissions.Editions.SetFeatures,
            Type = MenuType.Button,
            Sort = 986,
        });
        initList.Add(new Menu()
        {
            Id = 2506,
            Name = "查询",
            ParentId = 2501,
            PermissionCode = SaasPermissions.Editions.Search,
            Type = MenuType.Button,
            Sort = 985,
        });
        initList.Add(new Menu()
        {
            Id = 2507,
            Name = "详情",
            ParentId = 2501,
            PermissionCode = SaasPermissions.Editions.LookDetail,
            Type = MenuType.Button,
            Sort = 984,
        });
     
    }


    private void CreateAboutMenu(List<Menu> initList)
    {
        initList.Add(new Menu()
        {
            Id = 2300,
            Name = "关于",
            ParentId = null,
            RoutePath = "/about",
            Component = "LAYOUT",
            ExternalLink = false,
            Icon = "simple-icons:about-dot-me",
            Type = MenuType.Catalog,
            Sort = -1000,
        });
        initList.Add(new Menu()
        {
            Id = 2301,
            Name = "关于详情",
            ParentId = 2300,
            RoutePath = "/about",
            Component = "/about/index",
            ExternalLink = false,
            Type = MenuType.Menu,
            Sort = 997,
        });
    }

    private void CreateLogMenu(List<Menu> initList)
    {
        initList.Add(new Menu()
        {
            Id = 2200,
            Name = "日志管理",
            ParentId = null,
            RoutePath = "/log",
            Component = "LAYOUT",
            ExternalLink = false,
            Icon = "ant-design:audit-outlined",
            Type = MenuType.Catalog,
            Sort = 999,
        });
        initList.Add(new Menu()
        {
            Id = 2201,
            Name = "审计日志",
            ParentId = 2200,
            RoutePath = "/log/audit",
            Component = "/log-manage/audit/index",
            ExternalLink = false,
            Type = MenuType.Menu,
            Sort = 998,
        });
        initList.Add(new Menu()
        {
            Id = 2202,
            Name = "查询",
            ParentId = 2200,
            PermissionCode = LogPermissions.AuditLogging.Search,
            Type = MenuType.Button,
            Sort = 998,
        });
        initList.Add(new Menu()
        {
            Id = 2203,
            Name = "详情",
            ParentId = 2200,
            PermissionCode = LogPermissions.AuditLogging.LookDetail,
            Type = MenuType.Button,
            Sort = 997,
        });
    }

    private void CreateDashboardMenu(List<Menu> initList)
    {
        initList.Add(new Menu()
        {
            Id = 2100,
            Name = "Dashboard",
            ParentId = null,
            RoutePath = "/dashboard",
            Component = "LAYOUT",
            Icon = "ion:grid-outline",
            ExternalLink = false,
            Type = MenuType.Catalog,
            Sort = 1001,
            Display = true,
        });
        initList.Add(new Menu()
        {
            Id = 2101,
            Name = "分析页",
            ParentId = 2100,
            RoutePath = "/dashboard/analysis",
            Component = "/dashboard/analysis/index",
            ExternalLink = false,
            Type = MenuType.Menu,
            KeepAlive = true,
            Sort = 1000,
            Display = true,
        });
        initList.Add(new Menu()
        {
            Id = 2102,
            Name = "工作台",
            ParentId = 2100,
            RoutePath = "/dashboard/workbench",
            Component = "/dashboard/workbench/index",
            ExternalLink = false,
            Type = MenuType.Menu,
            KeepAlive = true,
            Sort = 999,
            Display = true,
        });
    }

    private void CreateRoleMenu(List<Menu> initList)
    {
        initList.Add(new Menu()
        {
            Id = 1100,
            Name = "角色管理",
            ParentId = 1,
            RoutePath = "/authorization/role",
            Component = "/authorization/role/index",
            ExternalLink = false,
            Type = MenuType.Menu,
            Sort = 1000,
            Display = true,
        });
        initList.Add(new Menu()
        {
            Id = 1102,
            Name = "新增",
            ParentId = 1100,
            PermissionCode = IdentityPermissions.Roles.Create,
            Type = MenuType.Button,
            Sort = 999,
        });
        initList.Add(new Menu()
        {
            Id = 1103,
            Name = "编辑",
            ParentId = 1100,
            PermissionCode = IdentityPermissions.Roles.Update,
            Type = MenuType.Button,
            Sort = 998,
        });
        initList.Add(new Menu()
        {
            Id = 1104,
            Name = "删除",
            ParentId = 1100,
            PermissionCode = IdentityPermissions.Roles.Delete,
            Type = MenuType.Button,
            Sort = 997,
        });
        initList.Add(new Menu()
        {
            Id = 1105,
            Name = "授权菜单",
            ParentId = 1100,
            PermissionCode = IdentityPermissions.Roles.SetMenus,
            Type = MenuType.Button,
            Sort = 996,
        });
        initList.Add(new Menu()
        {
            Id = 1106,
            Name = "授权数据",
            ParentId = 1100,
            PermissionCode = IdentityPermissions.Roles.SetMenus,
            Type = MenuType.Button,
            Sort = 995,
        });
        initList.Add(new Menu()
        {
            Id = 1107,
            Name = "查询",
            ParentId = 1100,
            PermissionCode = IdentityPermissions.Roles.Search,
            Type = MenuType.Button,
            Sort = 994,
        });
        initList.Add(new Menu()
        {
            Id = 1108,
            Name = "详情",
            ParentId = 1100,
            PermissionCode = IdentityPermissions.Roles.LookDetail,
            Type = MenuType.Button,
            Sort = 993,
        });
    }

    private void CreateMenuMenu(List<Menu> initList)
    {
        initList.Add(new Menu()
        {
            Id = 1001,
            Name = "菜单管理",
            ParentId = 1,
            RoutePath = "/authorization/menu",
            Component = "/authorization/menu/index",
            ExternalLink = false,
            Type = MenuType.Menu,
            Sort = 1000,
            Display = true,
        });
        initList.Add(new Menu()
        {
            Id = 1002,
            Name = "新增",
            ParentId = 1001,
            PermissionCode = PermissionPermissions.Menus.Create,
            Type = MenuType.Button,
            Sort = 999,
        });
        initList.Add(new Menu()
        {
            Id = 1003,
            Name = "编辑",
            ParentId = 1001,
            PermissionCode = PermissionPermissions.Menus.Update,
            Type = MenuType.Button,
            Sort = 998,
        });
        initList.Add(new Menu()
        {
            Id = 1004,
            Name = "删除",
            ParentId = 1001,
            PermissionCode = PermissionPermissions.Menus.Delete,
            Type = MenuType.Button,
            Sort = 997,
        });
        initList.Add(new Menu()
        {
            Id = 1005,
            Name = "查询",
            ParentId = 1001,
            PermissionCode = PermissionPermissions.Menus.Search,
            Type = MenuType.Button,
            Sort = 996,
        });
        initList.Add(new Menu()
        {
            Id = 1006,
            Name = "详情",
            ParentId = 1001,
            PermissionCode = PermissionPermissions.Menus.LookDetail,
            Type = MenuType.Button,
            Sort = 995,
        });
    }

    private void CreatePositionMenu(List<Menu> initList)
    {
        initList.Add(new Menu()
        {
            Id = 200,
            Name = "岗位管理",
            ParentId = 1,
            Type = MenuType.Menu,
            Sort = 1000,
            RoutePath = "/authorization/position",
            Component = "/authorization/position/index",
            ExternalLink = false,
            Display = true,
        });
        initList.Add(new Menu()
        {
            Id = 201,
            Name = "新增",
            ParentId = 200,
            PermissionCode = PositionPermissions.Positions.Create,
            Type = MenuType.Button,
            Sort = 999,
        });
        initList.Add(new Menu()
        {
            Id = 202,
            Name = "编辑",
            ParentId = 200,
            PermissionCode = PositionPermissions.Positions.Update,
            Type = MenuType.Button,
            Sort = 998,
        });
        initList.Add(new Menu()
        {
            Id = 203,
            Name = "删除",
            ParentId = 200,
            PermissionCode = PositionPermissions.Positions.Delete,
            Type = MenuType.Button,
            Sort = 997,
        });

        initList.Add(new Menu()
        {
            Id = 204,
            Name = "查询",
            ParentId = 200,
            PermissionCode = PositionPermissions.Positions.Search,
            Type = MenuType.Button,
            Sort = 996,
        });
        initList.Add(new Menu()
        {
            Id = 205,
            Name = "详情",
            ParentId = 200,
            PermissionCode = PositionPermissions.Positions.LookDetail,
            Type = MenuType.Button,
            Sort = 995,
        });
    }

    private static void CreateOrganizationMenu(List<Menu> initList)
    {
        initList.Add(new Menu()
        {
            Id = 100,
            Name = "机构管理",
            ParentId = 1,
            Type = MenuType.Menu,
            Sort = 1000,
            RoutePath = "/authorization/organization",
            Component = "/authorization/organization/index",
            ExternalLink = false,
            Display = true,
        });
        initList.Add(new Menu()
        {
            Id = 101,
            Name = "新增",
            ParentId = 100,
            PermissionCode = OrganizationPermissions.Organizations.Create,
            Type = MenuType.Button,
            Sort = 999,
        });
        initList.Add(new Menu()
        {
            Id = 102,
            Name = "编辑",
            ParentId = 100,
            PermissionCode = OrganizationPermissions.Organizations.Update,
            Type = MenuType.Button,
            Sort = 998,
        });
        initList.Add(new Menu()
        {
            Id = 103,
            Name = "删除",
            ParentId = 100,
            PermissionCode = OrganizationPermissions.Organizations.Delete,
            Type = MenuType.Button,
            Sort = 997,
        });
        initList.Add(new Menu()
        {
            Id = 104,
            Name = "新增成员",
            ParentId = 100,
            PermissionCode = OrganizationPermissions.Organizations.AddUsers,
            Type = MenuType.Button,
            Sort = 996,
        });
        initList.Add(new Menu()
        {
            Id = 105,
            Name = "移除成员",
            ParentId = 100,
            PermissionCode = OrganizationPermissions.Organizations.RemoveUser,
            Type = MenuType.Button,
            Sort = 995,
        });
        initList.Add(new Menu()
        {
            Id = 106,
            Name = "分配角色",
            ParentId = 100,
            PermissionCode = OrganizationPermissions.Organizations.AllocationRole,
            Type = MenuType.Button,
            Sort = 994,
        });
        initList.Add(new Menu()
        {
            Id = 107,
            Name = "分配职位",
            ParentId = 100,
            PermissionCode = OrganizationPermissions.Organizations.AllocationPosition,
            Type = MenuType.Button,
            Sort = 993,
        });
        
        initList.Add(new Menu()
        {
            Id = 108,
            Name = "详情",
            ParentId = 100,
            PermissionCode = OrganizationPermissions.Organizations.LookDetail,
            Type = MenuType.Button,
            Sort = 994,
        });
    }

    private static void CreateUserMenu(List<Menu> initList)
    {
        initList.Add(new Menu()
        {
            Id = 2,
            Name = "用户管理",
            ParentId = 1,
            Type = MenuType.Menu,
            RoutePath = "/authorization/user",
            Component = "/authorization/user/index",
            ExternalLink = false,
            KeepAlive = true,
            Display = true,
            Sort = 999,
        });

        initList.Add(new Menu()
        {
            Id = 3,
            Name = "新增",
            ParentId = 2,
            PermissionCode = IdentityPermissions.Users.Create,
            Type = MenuType.Button,
            Sort = 1000,
        });

        initList.Add(new Menu()
        {
            Id = 4,
            Name = "编辑",
            ParentId = 2,
            PermissionCode = IdentityPermissions.Users.Update,
            Type = MenuType.Button,
            Sort = 999,
        });

        initList.Add(new Menu()
        {
            Id = 5,
            Name = "删除",
            ParentId = 2,
            PermissionCode = IdentityPermissions.Users.Delete,
            Type = MenuType.Button,
            Sort = 998,
        });

        initList.Add(new Menu()
        {
            Id = 6,
            Name = "锁定",
            ParentId = 2,
            PermissionCode = IdentityPermissions.Users.Lock,
            Type = MenuType.Button,
            Sort = 997,
        });

        initList.Add(new Menu()
        {
            Id = 7,
            Name = "解锁",
            ParentId = 2,
            PermissionCode = IdentityPermissions.Users.UnLock,
            Type = MenuType.Button,
            Sort = 996,
        });

        initList.Add(new Menu()
        {
            Id = 8,
            Name = "更新声明",
            ParentId = 2,
            PermissionCode = IdentityPermissions.Users.UpdateClaimTypes,
            Type = MenuType.Button,
            Sort = 995,
        });

        initList.Add(new Menu()
        {
            Id = 9,
            Name = "授权角色",
            ParentId = 2,
            PermissionCode = IdentityPermissions.Users.SetRoles,
            Type = MenuType.Button,
            Sort = 994,
        });
        initList.Add(new Menu()
        {
            Id = 10,
            Name = "查询",
            ParentId = 2,
            PermissionCode = IdentityPermissions.Users.Search,
            Type = MenuType.Button,
            Sort = 993,
        });
        initList.Add(new Menu()
        {
            Id = 11,
            Name = "详情",
            ParentId = 2,
            PermissionCode = IdentityPermissions.Users.LookDetail,
            Type = MenuType.Button,
            Sort = 992,
        });
    }
}