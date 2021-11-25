using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Silky.Core.Exceptions;
using Silky.Hero.Common.Exceptions;

namespace Silky.Identity.Domain;

public class IdentityRoleManager: RoleManager<IdentityRole>
{
    public IdentityRoleManager(IdentityRoleStore store,
        IEnumerable<IRoleValidator<IdentityRole>> roleValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        ILogger<IdentityRoleManager> logger) 
        : base(store,
            roleValidators,
            keyNormalizer,
            errors,
            logger)
    {
    }
    
    public virtual async Task<IdentityRole> GetByIdAsync(long id)
    {
        var role = await Store.FindByIdAsync(id.ToString(), CancellationToken);
        if (role == null)
        {
            throw new EntityNotFoundException(typeof(IdentityRole), id);
        }

        return role;
    }

    public async override Task<IdentityResult> SetRoleNameAsync(IdentityRole role, string name)
    {
        if (role.IsStatic && role.Name != name)
        {
            throw new BusinessException("静态角色名称不允许重命名");
        }

        return await base.SetRoleNameAsync(role, name);
    }

    public async override Task<IdentityResult> DeleteAsync(IdentityRole role)
    {
        if (role.IsStatic)
        {
            throw new BusinessException("静态角色名称不允许删除");
        }

        return await base.DeleteAsync(role);
    }
}