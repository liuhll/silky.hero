using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Silky.Identity.Domain;

public class IdentityRoleStore:
    IRoleStore<IdentityRole>,
    IRoleClaimStore<IdentityRole>
{
    public void Dispose()
    {
        throw new System.NotImplementedException();
    }

    public Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<string> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task SetRoleNameAsync(IdentityRole role, string roleName, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<string> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task SetNormalizedRoleNameAsync(IdentityRole role, string normalizedName, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<IdentityRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<IdentityRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }

    public Task<IList<Claim>> GetClaimsAsync(IdentityRole role, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task AddClaimAsync(IdentityRole role, Claim claim, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }

    public Task RemoveClaimAsync(IdentityRole role, Claim claim, CancellationToken cancellationToken = new CancellationToken())
    {
        throw new System.NotImplementedException();
    }
}