using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Silky.Core.Extensions;
using Silky.Hero.Common;

namespace Silky.Identity.Domain;

public class IdentityUserValidator : UserValidator<IdentityUser>
{
    public override async Task<IdentityResult> ValidateAsync(UserManager<IdentityUser> manager, IdentityUser user)
    {
        if (manager == null)
        {
            throw new ArgumentNullException(nameof(manager));
        }

        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var errors = new List<IdentityError>();
        await ValidateUserName(manager, user, errors);
        await ValidatePhoneNumber(manager, user, errors);
        await ValidateJobMumber(manager, user, errors);
        if (manager.Options.User.RequireUniqueEmail)
        {
            await ValidateEmail(manager, user, errors);
        }

        return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;
    }

    private async Task ValidateJobMumber(UserManager<IdentityUser> manager, IdentityUser user, List<IdentityError> errors)
    {
        var jobNumber = await ((IdentityUserManager)manager).GetJobNumberAsync(user);
        if (jobNumber.IsNullOrWhiteSpace())
        {
            errors.Add(new IdentityError()
            {
                Code = "InvalidJobNumber",
                Description = "工号不允许为空"
            });
            return;
        }

        if (!Regex.IsMatch(jobNumber, RegularExpressionConsts.JobNumber))
        {
            errors.Add(new IdentityError()
            {
                Code = "InvalidJobNumber",
                Description = "工号格式不正确"
            });
            return;
        }

        var owner = await ((IdentityUserManager)manager).FindByJobNumberAsync(jobNumber);
        if (owner != null && !string.Equals(await manager.GetUserIdAsync(owner), await manager.GetUserIdAsync(user)))
        {
            errors.Add(new IdentityError()
            {
                Code = "DuplicateJobNumber",
                Description = "工号不允许重复"
            });
        }
    }

    private async Task ValidatePhoneNumber(UserManager<IdentityUser> manager, IdentityUser user,
        List<IdentityError> errors)
    {
        var phoneNumber = await manager.GetPhoneNumberAsync(user);
        if (phoneNumber.IsNullOrWhiteSpace())
        {
            errors.Add(new IdentityError()
            {
                Code = "InvalidPhoneNumber",
                Description = "手机号码不允许为空"
            });
            return;
        }

        if (!Regex.IsMatch(phoneNumber, RegularExpressionConsts.MobilePhone))
        {
            errors.Add(new IdentityError()
            {
                Code = "InvalidPhoneNumber",
                Description = "手机号码格式不正确"
            });
            return;
        }

        var owner = await ((IdentityUserManager)manager).FindByPhoneNumberAsync(phoneNumber);
        if (owner != null && !string.Equals(await manager.GetUserIdAsync(owner), await manager.GetUserIdAsync(user)))
        {
            errors.Add(new IdentityError()
            {
                Code = "DuplicatePhoneNumber",
                Description = "手机号码不允许重复"
            });
        }
    }

    private async Task ValidateEmail(UserManager<IdentityUser> manager, IdentityUser user, List<IdentityError> errors)
    {
        var email = await manager.GetEmailAsync(user);
        if (email.IsNullOrWhiteSpace())
        {
            errors.Add(new IdentityError()
            {
                Code = "InvalidEmail",
                Description = "电子邮件不允许为空"
            });
            return;
        }

        if (!new EmailAddressAttribute().IsValid(email))
        {
            errors.Add(new IdentityError()
            {
                Code = "InvalidEmail",
                Description = "电子邮件格式不正确"
            });
            return;
        }

        var owner = await manager.FindByEmailAsync(email);
        if (owner != null && !string.Equals(await manager.GetUserIdAsync(owner), await manager.GetUserIdAsync(user)))
        {
            errors.Add(new IdentityError()
            {
                Code = "DuplicateEmail",
                Description = "电子邮件不允许重复"
            });
        }
    }

    private async Task ValidateUserName(UserManager<IdentityUser> manager, IdentityUser user,
        ICollection<IdentityError> errors)
    {
        var userName = await manager.GetUserNameAsync(user);
        if (userName.IsNullOrWhiteSpace())
        {
            errors.Add(new IdentityError()
            {
                Code = "InvalidUserName",
                Description = "用户名不允许为空"
            });
        }
        else if (!manager.Options.User.AllowedUserNameCharacters.IsNullOrWhiteSpace() &&
                 userName.Any(c => !manager.Options.User.AllowedUserNameCharacters.Contains(c)))
        {
            errors.Add(new IdentityError()
            {
                Code = "InvalidUserName",
                Description = "用户名格式不正确"
            });
        }
        else
        {
            var owner = await manager.FindByNameAsync(userName);
            if (owner != null &&
                !string.Equals(await manager.GetUserIdAsync(owner), await manager.GetUserIdAsync(user)))
            {
                errors.Add(new IdentityError()
                {
                    Code = "DuplicateUserName",
                    Description = "用户名不允许重复"
                });
            }
        }
    }
}