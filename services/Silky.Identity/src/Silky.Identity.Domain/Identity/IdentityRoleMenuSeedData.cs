using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Entities.Configures;

namespace Silky.Identity.Domain.Identity;

public class IdentityRoleMenuSeedData : IEntitySeedData<IdentityRoleMenu>
{
    public IEnumerable<IdentityRoleMenu> HasData(DbContext dbContext, Type dbContextLocator)
    {
        var initDataList = new List<IdentityRoleMenu>();
        CreateAdminMenuData(initDataList);
        CreateNormMenuData(initDataList);
        return initDataList;
    }

    private void CreateNormMenuData(List<IdentityRoleMenu> initDataList)
    {
        var normMenuIds = GetNormMenuIds();
        var id = 10000;
        foreach (var menuId in normMenuIds)
        {
            initDataList.Add(new IdentityRoleMenu()
            {
                Id = id,
                MenuId = menuId,
                RoleId = 2,
                TenantId = 1,
                CreatedTime = DateTimeOffset.Now
            });
            id++;
        }
    }

    private void CreateAdminMenuData(List<IdentityRoleMenu> initDataList)
    {
        var adminMenuIds = GetAdminMenuIds();
        var id = 1;
        foreach (var menuId in adminMenuIds)
        {
            initDataList.Add(new IdentityRoleMenu()
            {
                Id = id,
                MenuId = menuId,
                RoleId = 1,
                TenantId = 1,
                CreatedTime = DateTimeOffset.Now
            });
            id++;
        }
    }

    private long[] GetNormMenuIds()
    {
        return new long[]
        {
            2,
            10,
            11,
            2100,
            2101,
            2102
        };
    }

    private long[] GetAdminMenuIds()
    {
        return new long[]
        {
            1,
            2,
            3,
            4,
            5,
            6,
            7,
            8,
            9,
            10,
            11,
            100,
            101,
            102,
            103,
            104,
            105,
            106,
            107,
            108,
            200,
            201,
            202,
            203,
            204,
            205,
            1001,
            1002,
            1003,
            1004,
            1005,
            1006,
            1100,
            1102,
            1103,
            1104,
            1105,
            1106,
            1107,
            1108,
            2100,
            2101,
            2102,
            2200,
            2201,
            2202,
            2203,
            2300,
            2301,
            2400,
            2401,
            2402,
            2403,
            2404,
            2405,
            2406,
            2407,
            2501,
            2502,
            2503,
            2504,
            2505,
            2506,
            2507,
            3000,
            3001,
            3002,
            3003,
            3004,
            3005,
            3006,
            3007,
            3008,
            10000,
            10001,
            10002,
            10003,
            10004
        };
    }
}