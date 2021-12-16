using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Entities.Configures;

namespace Silky.BasicData.Domain.Dictionary;

public class DictionaryItemSeedData : IEntitySeedData<DictionaryItem>
{
    public IEnumerable<DictionaryItem> HasData(DbContext dbContext, Type dbContextLocator)
    {
        var initList = new List<DictionaryItem>();

        initList.Add(new ()
        {
            Id = 1,
            DictionaryId = 1,
            Code = "male",
            Value = "1",
            Sort = 1,
        });
        
        initList.Add(new ()
        {
            Id = 2,
            DictionaryId = 1,
            Code = "female",
            Value = "0",
            Sort = 1,
        });
        
        return initList;
    }
}