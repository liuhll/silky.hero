﻿using System.Threading.Tasks;
using Silky.BasicData.Application.Contracts.Dictionary.Dtos;
using Silky.EntityFrameworkCore.Repositories;

namespace Silky.BasicData.Domain.Dictionary;

public interface IDictionaryDomainService
{
    IRepository<DictionaryType> DictionaryTypeRepository { get; }

    IRepository<DictionaryItem> DictionaryItemRepository { get; }
    
    Task CreateTypeAsync(CreateOrUpdateDictionaryTypeInput input);
    
    Task UpdateTypeAsync(CreateOrUpdateDictionaryTypeInput input);
    Task CreateItemAsync(CreateOrUpdateDictionaryItemInput input);
    Task UpdateItemAsync(CreateOrUpdateDictionaryItemInput input);
}