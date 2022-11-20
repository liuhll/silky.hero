using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Entities.Configures;
using Silky.Identity.Domain.Shared;

namespace Silky.Identity.Domain;

public class IdentityRoleSeedData : IEntitySeedData<IdentityRole>
{
    public IEnumerable<IdentityRole> HasData(DbContext dbContext, Type dbContextLocator)
    {
        var initRoles = new List<IdentityRole>()
        {
            new("admin", "管理员", 1) { Id = 1, IsPublic = true, IsStatic = true, DataRange = DataRange.Whole },
            new("normal", "普通用户", 1)
                { Id = 2, IsDefault = true, IsPublic = true, IsStatic = true, DataRange = DataRange.SelfOrganization }
        };
        return initRoles;
    }
}