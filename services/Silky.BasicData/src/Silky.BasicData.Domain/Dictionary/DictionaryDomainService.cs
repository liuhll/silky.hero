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

    public async Task CreateTypeAsync(CreateDictionaryTypeInput input)
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

    public async Task UpdateTypeAsync(CreateDictionaryTypeInput input)
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
}