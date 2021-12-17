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

    public Task CreateOrUpdateTypeAsync(CreateDictionaryTypeInput input)
    {
        if (!input.Id.HasValue)
        {
            return _dictionaryDomainService.CreateTypeAsync(input);
        }

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

    public Task CreateOrUpdateItemAsync(CreateDictionaryItemInput input)
    {
        if (!input.Id.HasValue)
        {
            return _dictionaryDomainService.CreateItemAsync(input);
        }

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
    }
}