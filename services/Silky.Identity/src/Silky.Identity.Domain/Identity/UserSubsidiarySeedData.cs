using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Entities.Configures;

namespace Silky.Identity.Domain.Identity;

public class UserSubsidiarySeedData : IEntitySeedData<UserSubsidiary>
{
    public IEnumerable<UserSubsidiary> HasData(DbContext dbContext, Type dbContextLocator)
    {
        var initDataList = new List<UserSubsidiary>()
        {
            new ()
            {
                Id = 1,
                UserId = 1,
                OrganizationId = 1,
                PositionId = 1,
                TenantId = 1,
            },
            new ()
            {
                Id = 2,
                UserId = 2,
                OrganizationId = 2,
                PositionId = 2,
                TenantId = 1,
            },
            new ()
            {
                Id = 3,
                UserId = 3,
                OrganizationId = 3,
                PositionId = 3,
                TenantId = 1,
            }
        };
        return initDataList;
    }
}