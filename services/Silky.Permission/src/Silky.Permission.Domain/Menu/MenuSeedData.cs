using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Entities.Configures;

namespace Silky.Permission.Domain.Menu;

public class MenuSeedData : IEntitySeedData<Menu>
{
    public IEnumerable<Menu> HasData(DbContext dbContext, Type dbContextLocator)
    {
        var initList = new List<Menu>();
        
        return initList;
    }
}