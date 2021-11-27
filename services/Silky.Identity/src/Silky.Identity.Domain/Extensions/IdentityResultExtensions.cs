using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ValidationException = Silky.Core.Exceptions.ValidationException;

namespace Microsoft.AspNetCore.Identity;

public static class IdentityResultExtensions
{
    public static void CheckErrors(this IdentityResult identityResult)
    {
        if (identityResult.Succeeded)
        {
            return;
        }

        if (identityResult.Errors == null)
        {
            throw new ArgumentException("identityResult.Errors不允许为空.");
        }

        var errorMessage = string.Empty;
        var validResult = new List<ValidationResult>();
        foreach (var error in identityResult.Errors)
        {
            errorMessage += error.Description + ";";
            validResult.Add(new ValidationResult(error.Description));
        }
        throw new ValidationException(errorMessage, validResult);
    }
}