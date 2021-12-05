using System.Threading.Tasks;
using Mapster;
using Silky.Core.Exceptions;
using Silky.EntityFrameworkCore.Repositories;
using Silky.Identity.Application.Contracts.ClaimType;
using Silky.Identity.Application.Contracts.ClaimType.Dtos;
using Silky.Identity.Domain;

namespace Silky.Identity.Application.ClaimType;

public class ClaimTypeAppService : IClaimTypeAppService
{
    private readonly IRepository<IdentityClaimType> _identityClaimTypeRepository;

    public ClaimTypeAppService(IRepository<IdentityClaimType> identityClaimTypeRepository)
    {
        _identityClaimTypeRepository = identityClaimTypeRepository;
    }

    public async Task CreateOrUpdateAsync(CreateOrUpdateClaimTypeInput input)
    {
        var claimType = !input.Id.HasValue
            ? new IdentityClaimType(input.Name,
                input.Required,
                input.IsStatic,
                input.Regex,
                input.RegexDescription,
                input.Description,
                input.ValueType)
            : await _identityClaimTypeRepository.FindAsync(input.Id.Value);
        await UpdateClaimTypeByInput(claimType, input);
        _ = !input.Id.HasValue
            ? await _identityClaimTypeRepository.InsertAsync(claimType)
            : await _identityClaimTypeRepository.UpdateAsync(claimType);
    }

    private async Task UpdateClaimTypeByInput(IdentityClaimType claimType, CreateOrUpdateClaimTypeInput input)
    {
        if (!input.Id.HasValue || claimType.Name != input.Name)
        {
            var existClaimType = await _identityClaimTypeRepository.FirstOrDefaultAsync(p => p.Name == input.Name);
            if (existClaimType != null)
            {
                throw new UserFriendlyException($"已经存在{input.Name}的声明类型");
            }
        }

        if (input.Id.HasValue)
        {
            claimType = input.Adapt(claimType);
        }
    }
}