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
            new("name", isStatic: true) { Id = 1 },
            new("email", isStatic: true) { Id = 2 },
            new("sex", isStatic: true) { Id = 3 },
            new("mobilePhone", isStatic: true) { Id = 4 },
            new("telPhone", isStatic: true) { Id = 5 },
            new("surname", isStatic: true) { Id = 6 },
            new("realName", isStatic: true) { Id = 7 },
            new("jobNumber", isStatic: true) { Id = 8 },
        };
        return initClaimTypes;
    }
}