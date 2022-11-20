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

        var admin = new IdentityUser("admin", "admin@silky.com", "13111111111", 1)
        {
            Id = 1,
            RealName = "管理员",
            JobNumber = "0001",
        };
        var passwordHasher = new PasswordHasher<IdentityUser>();
        admin.PasswordHash =
            passwordHasher.HashPassword(admin, "123qweR!");
        initUsers.Add(admin);
        var liuhll = new IdentityUser("liuhll", "liuhll@silky.com", "13111111112", 1)
        {
            Id = 2,
            RealName = "Liuhll",
            JobNumber = "0002"
        };
        liuhll.PasswordHash =
            passwordHasher.HashPassword(liuhll, "123qweR!");
        initUsers.Add(liuhll);
        var lanchong = new IdentityUser("lanchong", "lanchong@silky.com", "13111111113", 1)
        {
            Id = 3,
            RealName = "懒虫",
            JobNumber = "0003"
        };
        lanchong.PasswordHash =
            passwordHasher.HashPassword(lanchong, "123qweR!");
        initUsers.Add(lanchong);
        return initUsers;
    }
}