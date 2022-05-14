using Silky.Rpc.Runtime.Server;

namespace Silky.Saas.Application.Contracts.Edition.Dtos;

public class UpdateEditionInput : EditionDtoBase
{
    [CacheKey(0)]
    public long Id { get; set; }
    
}