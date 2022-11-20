using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Entities.Configures;

namespace Silky.Identity.Domain.Identity;

public class IdentityUserRoleSeedData : IEntitySeedData<IdentityUserRole>
{
    public IEnumerable<IdentityUserRole> HasData(DbContext dbContext, Type dbContextLocator)
    {
        var initDataList = new List<IdentityUserRole>
        {
            new()
            {
                Id = 1,
                UserId = 1,
                RoleId = 1,
                TenantId = 1,
                CreatedTime = DateTimeOffset.Now
            },
            new()
            {
                Id = 2,
                UserId = 2,
                RoleId = 2,
                TenantId = 1,
                CreatedTime = DateTimeOffset.Now
            }
        };
        return initDataList;
    }
}