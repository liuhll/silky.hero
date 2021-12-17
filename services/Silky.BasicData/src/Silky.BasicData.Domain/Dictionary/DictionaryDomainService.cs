using System.Threading.Tasks;
using Mapster;
using Silky.BasicData.Application.Contracts.Dictionary.Dtos;
using Silky.Core;
using Silky.Core.DependencyInjection;
using Silky.Core.Exceptions;
using Silky.EntityFrameworkCore.Repositories;

namespace Silky.BasicData.Domain.Dictionary;

public class DictionaryDomainService : IDictionaryDomainService, IScopedDependency
{
    public DictionaryDomainService(
        IRepository<DictionaryType> dictionaryTypeRepository,
        IRepository<DictionaryItem> dictionaryItemRepository)
    {
        DictionaryTypeRepository = dictionaryTypeRepository;
        DictionaryItemRepository = dictionaryItemRepository;
    }

    public IRepository<DictionaryType> DictionaryTypeRepository { get; }

    public IRepository<DictionaryItem> DictionaryItemRepository { get; }

    public async Task CreateTypeAsync(CreateOrUpdateDictionaryTypeInput input)
    {
        var existDictType = await DictionaryTypeRepository.FirstOrDefaultAsync(p => p.Code == input.Code);
        if (existDictType != null)
        {
            throw new UserFriendlyException($"已经存在Code为{input.Code}的字典类型");
        }

        existDictType = await DictionaryTypeRepository.FirstOrDefaultAsync(p => p.Name == input.Name);
        if (existDictType != null)
        {
            throw new UserFriendlyException($"已经存在名称为{input.Name}的字典类型");
        }

        var dictType = input.Adapt<DictionaryType>();
        await DictionaryTypeRepository.InsertAsync(dictType);
    }

    public async Task UpdateTypeAsync(CreateOrUpdateDictionaryTypeInput input)
    {
        Check.NotNull(input.Id, nameof(input.Id));
        var dictType = await DictionaryTypeRepository.FindOrDefaultAsync(input.Id);
        if (dictType == null)
        {
            throw new UserFriendlyException($"不已经存在id为{input.Id}的字典类型");
        }

        if (!dictType.Code.Equals(input.Code))
        {
            var existDictType = await DictionaryTypeRepository.FirstOrDefaultAsync(p => p.Code == input.Code);
            if (existDictType != null)
            {
                throw new UserFriendlyException($"已经存在Code为{input.Code}的字典类型");
            }
        }

        if (!dictType.Name.Equals(input.Name))
        {
            var existDictType = await DictionaryTypeRepository.FirstOrDefaultAsync(p => p.Name == input.Name);
            if (existDictType != null)
            {
                throw new UserFriendlyException($"已经存在名称为{input.Name}的字典类型");
            }
        }

        dictType = input.Adapt(dictType);
        await DictionaryTypeRepository.UpdateAsync(dictType);
    }

    public async Task CreateItemAsync(CreateOrUpdateDictionaryItemInput input)
    {
        await CheckExistDictionaryType(input.DictionaryId);
        var existDictItem = await DictionaryItemRepository.FirstOrDefaultAsync(p =>
            p.Code == input.Code && p.DictionaryId == input.DictionaryId);
        if (existDictItem != null)
        {
            throw new UserFriendlyException($"已经存在Code为{input.Code}的字典项目");
        }

        existDictItem = await DictionaryItemRepository.FirstOrDefaultAsync(p =>
            p.Value == input.Value && p.DictionaryId == input.DictionaryId);
        if (existDictItem != null)
        {
            throw new UserFriendlyException($"已经存在值为{input.Value}的字典值");
        }

        var dictItem = input.Adapt<DictionaryItem>();
        await DictionaryItemRepository.InsertAsync(dictItem);
    }

    public async Task UpdateItemAsync(CreateOrUpdateDictionaryItemInput input)
    {
        await CheckExistDictionaryType(input.DictionaryId);
        Check.NotNull(input.Id, nameof(input.Id));
        var dictItem = await DictionaryItemRepository.FindOrDefaultAsync(input.Id);
        if (dictItem == null)
        {
            throw new UserFriendlyException($"不已经存在id为{input.Id}的字典类型");
        }

        if (dictItem.DictionaryId != input.DictionaryId)
        {
            throw new UserFriendlyException($"指定的字典Id{input.DictionaryId}不正确");
        }

        if (!dictItem.Code.Equals(input.Code))
        {
            var existDictType = await DictionaryItemRepository.FirstOrDefaultAsync(p =>
                p.Code == input.Code && p.DictionaryId == input.DictionaryId);
            if (existDictType != null)
            {
                throw new UserFriendlyException($"已经存在Code为{input.Code}的字典项");
            }
        }

        if (!dictItem.Value.Equals(input.Value))
        {
            var existDictType = await DictionaryItemRepository.FirstOrDefaultAsync(p =>
                p.Value == input.Value && p.DictionaryId == input.DictionaryId);
            if (existDictType != null)
            {
                throw new UserFriendlyException($"已经存在值为{input.Value}的字典值");
            }
        }

        dictItem = input.Adapt(dictItem);
        await DictionaryItemRepository.UpdateAsync(dictItem);
    }

    private async Task CheckExistDictionaryType(long id)
    {
        var dictType = await DictionaryTypeRepository.FindOrDefaultAsync(id);
        if (dictType == null)
        {
            throw new UserFriendlyException($"不存在Id为{id}的字典类型");
        }
    }
}