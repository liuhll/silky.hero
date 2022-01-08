using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Entities.Configures;

namespace Silky.Identity.Domain;

public class IdentityRoleSeedData : IEntitySeedData<IdentityRole>
{
    public IEnumerable<IdentityRole> HasData(DbContext dbContext, Type dbContextLocator)
    {
        var initRoles = new List<IdentityRole>()
        {
            new ("admin","管理员",1) { Id = 1},
            new ("normal","一般角色",1) { Id = 2}
        };
        return initRoles;
    }
}