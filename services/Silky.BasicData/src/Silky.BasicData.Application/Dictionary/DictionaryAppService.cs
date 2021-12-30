using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Silky.BasicData.Application.Contracts.Dictionary;
using Silky.BasicData.Application.Contracts.Dictionary.Dtos;
using Silky.BasicData.Domain.Dictionary;
using Silky.Core.Exceptions;
using Silky.Core.Extensions;
using Silky.EntityFrameworkCore.Extensions;

namespace Silky.BasicData.Application.Dictionary;

public class DictionaryAppService : IDictionaryAppService
{
    private readonly IDictionaryDomainService _dictionaryDomainService;

    public DictionaryAppService(IDictionaryDomainService dictionaryDomainService)
    {
        _dictionaryDomainService = dictionaryDomainService;
    }
    
    public Task CreateTypeAsync(CreateDictionaryTypeInput input)
    {
        return _dictionaryDomainService.CreateTypeAsync(input);
    }

    public Task UpdateTypeAsync(UpdateDictionaryTypeInput input)
    {
        return _dictionaryDomainService.UpdateTypeAsync(input);
    }

    public async Task<GetDictionaryTypeOutput> GetTypeAsync(long id)
    {
        var dictType = await _dictionaryDomainService.DictionaryTypeRepository.FindOrDefaultAsync(id);
        if (dictType == null)
        {
            throw new UserFriendlyException($"不存在Id为{id}的字典类型");
        }

        return dictType.Adapt<GetDictionaryTypeOutput>();
    }

    public async Task DeleteTypeAsync(long id)
    {
        var dictType = await _dictionaryDomainService
            .DictionaryTypeRepository
            .Include(p => p.DictionaryItems)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (dictType == null)
        {
            throw new UserFriendlyException($"不存在Id为{id}的字典类型");
        }

        await _dictionaryDomainService.DictionaryTypeRepository.DeleteAsync(dictType);
        await _dictionaryDomainService.RemoveDictionaryItemsCache(dictType.Id);
    }

    public async Task<PagedList<GetDictionaryTypePageOutput>> GetTypePageAsync(GetDictionaryTypePageInput input)
    {
        return await _dictionaryDomainService.DictionaryTypeRepository
            .Where(!input.Code.IsNullOrEmpty(), p => p.Code.Contains(input.Code))
            .Where(!input.Name.IsNullOrEmpty(), p => p.Name.Contains(input.Name))
            .AsNoTracking()
            .ProjectToType<GetDictionaryTypePageOutput>()
            .ToPagedListAsync(input.PageIndex, input.PageSize);
    }

    public Task CreateItemAsync(CreateDictionaryItemInput input)
    {
        return _dictionaryDomainService.CreateItemAsync(input);
    }

    public Task UpdateItemAsync(UpdateDictionaryItemInput input)
    {
        return _dictionaryDomainService.UpdateItemAsync(input);
    }

    public async Task<GetDictionaryItemOutput> GetItemAsync(long id)
    {
        var dictItem = await _dictionaryDomainService.DictionaryItemRepository.FindOrDefaultAsync(id);
        if (dictItem == null)
        {
            throw new UserFriendlyException($"不存在Id为{id}的字典项");
        }

        return dictItem.Adapt<GetDictionaryItemOutput>();
    }

    public async Task DeleteItemAsync(long id)
    {
        var dictItem = await _dictionaryDomainService.DictionaryItemRepository.FindOrDefaultAsync(id);
        if (dictItem == null)
        {
            throw new UserFriendlyException($"不存在Id为{id}的字典项");
        }

        await _dictionaryDomainService.DictionaryItemRepository.DeleteAsync(dictItem);
        await _dictionaryDomainService.RemoveDictionaryItemsCache(dictItem.DictionaryId);
    }

    public async Task<PagedList<GetDictionaryItemPageOutput>> GetItemPageAsync(long dictionaryId,
        GetDictionaryItemPageInput input)
    {
        return await _dictionaryDomainService.DictionaryItemRepository
            .Where(p => p.DictionaryId == dictionaryId)
            .Where(!input.Code.IsNullOrEmpty(), p => p.Code.Contains(input.Code))
            .Where(!input.Value.IsNullOrEmpty(), p => p.Value.Contains(input.Value))
            .AsNoTracking()
            .ProjectToType<GetDictionaryItemPageOutput>()
            .ToPagedListAsync(input.PageIndex, input.PageSize);
    }

    public async Task<ICollection<GetDictionaryItemOutput>> GetAllItemsByIdAsync(long dictionaryId)
    {
        return await _dictionaryDomainService.DictionaryItemRepository
            .Where(p => p.DictionaryId == dictionaryId)
            .AsNoTracking()
            .ProjectToType<GetDictionaryItemOutput>()
            .ToListAsync();
    }

    public async Task<ICollection<GetDictionaryItemOutput>> GetAllItemsByCodeAsync(string code)
    {
        var dictType = await _dictionaryDomainService.DictionaryTypeRepository.FirstOrDefaultAsync(p => p.Code == code);
        if (dictType == null)
        {
            throw new UserFriendlyException($"不存在code为{code}的字典");
        }

        return await GetAllItemsByIdAsync(dictType.Id);
    }
}