using System.Collections.Generic;
using System.Threading.Tasks;
using Silky.Permission.Domain.Shared.Permission;

namespace Silky.Permission.Domain;

public interface IPermissionManager
{
    Task<bool> CheckAsync(ICollection<AuthorizeData> authorizeDatas);
}