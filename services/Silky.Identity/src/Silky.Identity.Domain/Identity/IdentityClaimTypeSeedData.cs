using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Entities.Configures;

namespace Silky.Identity.Domain;

public class IdentityClaimTypeSeedData : IEntitySeedData<IdentityClaimType>
{
    public IEnumerable<IdentityClaimType> HasData(DbContext dbContext, Type dbContextLocator)
    {
        var initClaimTypes = new List<IdentityClaimType>()
        {
            new("name", isStatic: true, tenantId: 1) { Id = 1 },
            new("email", isStatic: true, tenantId: 1) { Id = 2 },
            new("sex", isStatic: true, tenantId: 1) { Id = 3 },
            new("mobilePhone", isStatic: true, tenantId: 1) { Id = 4 },
            new("telPhone", isStatic: true, tenantId: 1) { Id = 5 },
            new("surname", isStatic: true, tenantId: 1) { Id = 6 },
            new("realName", isStatic: true, tenantId: 1) { Id = 7 },
            new("jobNumber", isStatic: true, tenantId: 1) { Id = 8 },
        };
        return initClaimTypes;
    }
}