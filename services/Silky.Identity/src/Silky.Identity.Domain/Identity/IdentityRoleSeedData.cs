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
            new ("admin","管理员") { Id = 1},
            new IdentityRole("normal","一般角色") { Id = 2}
        };
        return initRoles;
    }
}