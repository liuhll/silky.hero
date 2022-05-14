using System.Collections.Generic;
using Silky.Permission.Domain.Shared.Permission;
using Silky.Rpc.Runtime.Server;

namespace Silky.Permission.Application.Contracts.Permission.Dtos;

public class AuthorizeInput
{
    [CacheKey(0)]
    public string ServiceEntryId { get; set; }

    public ICollection<AuthorizeData> AuthorizeDatas { get; set; }

}