using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Silky.Core;

namespace Silky.Identity.Domain;

public class IdentityRoleStore :
    IRoleStore<IdentityRole>,
    IRoleClaimStore<IdentityRole>
{
    protected IIdentityRoleRepository RoleRepository { get; }

    protected ILogger<IdentityRoleStore> Logger { get; }

    public IdentityErrorDescriber ErrorDescriber { get; set; }

    public IdentityRoleStore(
        IIdentityRoleRepository roleRepository,
        ILogger<IdentityRoleStore> logger,
        IdentityErrorDescriber describer = null)
    {
        RoleRepository = roleRepository;
        Logger = logger;

        ErrorDescriber = describer ?? new IdentityErrorDescriber();
    }


    public async Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Check.NotNull(role, nameof(role));

        if (role.Id == default)
        {
            await RoleRepository.InsertNowAsync(role, cancellationToken: cancellationToken);
        }

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Check.NotNull(role, nameof(role));

        try
        {
            await RoleRepository.UpdateNowAsync(role, cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogWarning(ex.ToString()); //Trigger some AbpHandledException event
            return IdentityResult.Failed(ErrorDescriber.ConcurrencyFailure());
        }

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Check.NotNull(role, nameof(role));

        try
        {
            await RoleRepository.DeleteNowAsync(role, cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogWarning(ex.ToString()); //Trigger some AbpHandledException event
            return IdentityResult.Failed(ErrorDescriber.ConcurrencyFailure());
        }

        return IdentityResult.Success;
    }

    public Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Check.NotNull(role, nameof(role));

        return Task.FromResult(role.Id.ToString());
    }

    public Task<string> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Check.NotNull(role, nameof(role));

        return Task.FromResult(role.Name);
    }

    public Task SetRoleNameAsync(IdentityRole role, string roleName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Check.NotNull(role, nameof(role));

        role.ChangeName(roleName);
        return Task.CompletedTask;
    }

    public Task<string> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Check.NotNull(role, nameof(role));

        return Task.FromResult(role.NormalizedName);
    }

    public Task SetNormalizedRoleNameAsync(IdentityRole role, string normalizedName,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Check.NotNull(role, nameof(role));

        role.NormalizedName = normalizedName;

        return Task.CompletedTask;
    }

    public Task SetRoleRealNameAsync(IdentityRole role, string realName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Check.NotNull(role, nameof(role));

        role.ChangeRealName(realName);
        return Task.CompletedTask;
    }

    public Task<IdentityRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return RoleRepository.FindAsync(long.Parse(roleId), cancellationToken: cancellationToken);
    }

    public Task<IdentityRole> FindByNameAsync(string normalizedName, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Check.NotNull(normalizedName, nameof(normalizedName));

        return RoleRepository.FindByNormalizedNameAsync(normalizedName, cancellationToken: cancellationToken);
    }

    public async Task<IList<Claim>> GetClaimsAsync(IdentityRole role,
        CancellationToken cancellationToken = new CancellationToken())
    {
        cancellationToken.ThrowIfCancellationRequested();

        Check.NotNull(role, nameof(role));

        await RoleRepository.EnsureCollectionLoadedAsync(role, r => r.Claims, cancellationToken);

        return role.Claims.Select(c => c.ToClaim()).ToList();
    }

    public async Task AddClaimAsync(IdentityRole role, Claim claim,
        CancellationToken cancellationToken = new CancellationToken())
    {
        cancellationToken.ThrowIfCancellationRequested();

        Check.NotNull(role, nameof(role));
        Check.NotNull(claim, nameof(claim));

        await RoleRepository.EnsureCollectionLoadedAsync(role, r => r.Claims, cancellationToken);

        role.AddClaim(claim);
    }

    public async Task RemoveClaimAsync(IdentityRole role, Claim claim,
        CancellationToken cancellationToken = new CancellationToken())
    {
        Check.NotNull(role, nameof(role));
        Check.NotNull(claim, nameof(claim));

        await RoleRepository.EnsureCollectionLoadedAsync(role, r => r.Claims, cancellationToken);

        role.RemoveClaim(claim);
    }

    public void Dispose()
    {
    }
}