using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.Hero.Common.Enums;
using Silky.Identity.Domain.Shared;
using Silky.Organization.Domain.Shared;
using Silky.Permission.Domain.Shared;
using Silky.Permission.Domain.Shared.Menu;
using Silky.Position.Domain.Shared;

namespace Silky.Permission.Domain.Menu;

public class MenuSeedData : IEntitySeedData<Menu>
{
    public IEnumerable<Menu> HasData(DbContext dbContext, Type dbContextLocator)
    {
        var initList = new List<Menu>();
        initList.Add(new Menu()
        {
            Id = 1,
            Name = "组织架构",
            Type = MenuType.Catalog,
            ParentId = null,
            Sort = 999,
            Status = Status.Valid,
        });
        CreateUserMenu(initList);

        CreateOrganizationMenu(initList);

        CreatePositionMenu(initList);

        initList.Add(new Menu()
        {
            Id = 1000,
            Name = "权限管理",
            Type = MenuType.Catalog,
            ParentId = null,
            Sort = 998,
            Status = Status.Valid,
        });

        CreateMenuMenu(initList);
        
        CreateRoleMenu(initList);

        return initList;
    }

    private void CreateRoleMenu(List<Menu> initList)
    {
        initList.Add(new Menu()
        {
            Id = 1100,
            Name = "角色管理",
            ParentId = 1000,
            PermissionCode = IdentityPermissions.Roles.Default,
            Type = MenuType.Menu,
            Sort = 998,
            Status = Status.Valid,
        });
        initList.Add(new Menu()
        {
            Id = 1102,
            Name = "新增",
            ParentId = 1100,
            PermissionCode = IdentityPermissions.Roles.Create,
            Type = MenuType.Button,
            Status = Status.Valid,
        });
        initList.Add(new Menu()
        {
            Id = 1103,
            Name = "编辑",
            ParentId = 1100,
            PermissionCode = IdentityPermissions.Roles.Update,
            Type = MenuType.Button,
            Status = Status.Valid,
        });
        initList.Add(new Menu()
        {
            Id = 1104,
            Name = "删除",
            ParentId = 1100,
            PermissionCode = IdentityPermissions.Roles.Delete,
            Type = MenuType.Button,
            Status = Status.Valid,
        });
        initList.Add(new Menu()
        {
            Id = 1105,
            Name = "授权菜单",
            ParentId = 1100,
            PermissionCode = IdentityPermissions.Roles.SetMenus,
            Type = MenuType.Button,
            Status = Status.Valid,
        });
        initList.Add(new Menu()
        {
            Id = 1106,
            Name = "授权数据",
            ParentId = 1100,
            PermissionCode = IdentityPermissions.Roles.SetMenus,
            Type = MenuType.Button,
            Status = Status.Valid,
        });
    }

    private void CreateMenuMenu(List<Menu> initList)
    {
        initList.Add(new Menu()
        {
            Id = 1001,
            Name = "菜单管理",
            ParentId = 1000,
            PermissionCode = PermissionPermissions.Menus.Default,
            Type = MenuType.Menu,
            Sort = 999,
            Status = Status.Valid,
        });
        initList.Add(new Menu()
        {
            Id = 1002,
            Name = "新增",
            ParentId = 1001,
            PermissionCode = PermissionPermissions.Menus.Create,
            Type = MenuType.Button,
            Status = Status.Valid,
        });
        initList.Add(new Menu()
        {
            Id = 1003,
            Name = "编辑",
            ParentId = 1001,
            PermissionCode = PermissionPermissions.Menus.Update,
            Type = MenuType.Button,
            Status = Status.Valid,
        });
        initList.Add(new Menu()
        {
            Id = 1004,
            Name = "删除",
            ParentId = 1001,
            PermissionCode = PermissionPermissions.Menus.Delete,
            Type = MenuType.Button,
            Status = Status.Valid,
        });
    }

    private void CreatePositionMenu(List<Menu> initList)
    {
        initList.Add(new Menu()
        {
            Id = 200,
            Name = "职位管理",
            ParentId = 1,
            PermissionCode = PositionPermissions.Positions.Default,
            Type = MenuType.Menu,
            Sort = 997,
            Status = Status.Valid,
        });
        initList.Add(new Menu()
        {
            Id = 201,
            Name = "新增",
            ParentId = 200,
            PermissionCode = PositionPermissions.Positions.Create,
            Type = MenuType.Button,
            Status = Status.Valid,
        });
        initList.Add(new Menu()
        {
            Id = 202,
            Name = "编辑",
            ParentId = 200,
            PermissionCode = PositionPermissions.Positions.Update,
            Type = MenuType.Button,
            Status = Status.Valid,
        });
        initList.Add(new Menu()
        {
            Id = 203,
            Name = "删除",
            ParentId = 200,
            PermissionCode = PositionPermissions.Positions.Delete,
            Type = MenuType.Button,
            Status = Status.Valid,
        });
    }

    private static void CreateOrganizationMenu(List<Menu> initList)
    {
        initList.Add(new Menu()
        {
            Id = 100,
            Name = "机构管理",
            ParentId = 1,
            PermissionCode = OrganizationPermissions.Organizations.Default,
            Type = MenuType.Menu,
            Sort = 998,
            Status = Status.Valid,
        });
        initList.Add(new Menu()
        {
            Id = 101,
            Name = "新增",
            ParentId = 100,
            PermissionCode = OrganizationPermissions.Organizations.Create,
            Type = MenuType.Button,
            Status = Status.Valid,
        });
        initList.Add(new Menu()
        {
            Id = 102,
            Name = "编辑",
            ParentId = 100,
            PermissionCode = OrganizationPermissions.Organizations.Update,
            Type = MenuType.Button,
            Status = Status.Valid,
        });
        initList.Add(new Menu()
        {
            Id = 103,
            Name = "删除",
            ParentId = 100,
            PermissionCode = OrganizationPermissions.Organizations.Delete,
            Type = MenuType.Button,
            Status = Status.Valid,
        });
    }

    private static void CreateUserMenu(List<Menu> initList)
    {
        initList.Add(new Menu()
        {
            Id = 2,
            Name = "用户管理",
            ParentId = 1,
            PermissionCode = IdentityPermissions.Users.Default,
            Type = MenuType.Menu,
            Sort = 999,
            Status = Status.Valid,
        });

        initList.Add(new Menu()
        {
            Id = 3,
            Name = "新增",
            ParentId = 2,
            PermissionCode = IdentityPermissions.Users.Create,
            Type = MenuType.Button,
            Status = Status.Valid,
        });

        initList.Add(new Menu()
        {
            Id = 4,
            Name = "编辑",
            ParentId = 2,
            PermissionCode = IdentityPermissions.Users.Update,
            Type = MenuType.Button,
            Status = Status.Valid,
        });

        initList.Add(new Menu()
        {
            Id = 5,
            Name = "删除",
            ParentId = 2,
            PermissionCode = IdentityPermissions.Users.Delete,
            Type = MenuType.Button,
            Status = Status.Valid,
        });

        initList.Add(new Menu()
        {
            Id = 6,
            Name = "锁定",
            ParentId = 2,
            PermissionCode = IdentityPermissions.Users.Lock,
            Type = MenuType.Button,
            Status = Status.Valid,
        });

        initList.Add(new Menu()
        {
            Id = 7,
            Name = "解锁",
            ParentId = 2,
            PermissionCode = IdentityPermissions.Users.UnLock,
            Type = MenuType.Button,
            Status = Status.Valid,
        });

        initList.Add(new Menu()
        {
            Id = 8,
            Name = "更新声明",
            ParentId = 2,
            PermissionCode = IdentityPermissions.Users.UpdateClaimTypes,
            Type = MenuType.Button,
            Status = Status.Valid,
        });
    }
}