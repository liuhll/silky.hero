using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Silky.EntityFrameworkCore.Entities.Configures;

namespace Silky.Identity.Domain;

public class IdentityUserSeedData : IEntitySeedData<IdentityUser>
{
    public IEnumerable<IdentityUser> HasData(DbContext dbContext, Type dbContextLocator)
    {
        var initUsers = new List<IdentityUser>();

        var admin = new IdentityUser("admin", "admin@silky.com", "13111111111")
        {
            Id = 1,
        };
        var passwordHasher = new PasswordHasher<IdentityUser>();
        admin.PasswordHash =
            passwordHasher.HashPassword(admin, "123qweR!");
        initUsers.Add(admin);
        var liuhll = new IdentityUser("liuhll", "liuhll@silky.com", "13111111112")
        {
            Id = 2
        };
        liuhll.PasswordHash =
            passwordHasher.HashPassword(admin, "123qweR!");
        initUsers.Add(liuhll);
        return initUsers;
    }
}