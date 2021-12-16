using System.Threading.Tasks;
using Silky.BasicData.Application.Contracts.Dictionary;
using Silky.BasicData.Application.Contracts.Dictionary.Dtos;
using Silky.BasicData.Domain.Dictionary;

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
}