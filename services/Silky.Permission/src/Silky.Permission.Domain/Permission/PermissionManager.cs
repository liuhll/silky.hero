using System.Collections.Generic;
using System.Threading.Tasks;
using Silky.Core.DependencyInjection;
using Silky.Permission.Domain.Shared.Permission;

namespace Silky.Permission.Domain;

public class PermissionManager : IPermissionManager, IScopedDependency
{

    public Task<bool> CheckAsync(ICollection<AuthorizeData> authorizeDatas)
    {
        return Task.FromResult(true);
    }
}