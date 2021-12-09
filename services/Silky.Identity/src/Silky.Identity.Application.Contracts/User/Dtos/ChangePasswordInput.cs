using System.ComponentModel.DataAnnotations;
using Silky.Identity.Domain.Shared;

namespace Silky.Identity.Application.Contracts.User.Dtos;

public class ChangePasswordInput
{
    [RegularExpression(RegularExpressionConsts.Password, ErrorMessage = "密码格式不正确")]
    [Required(ErrorMessage = "密码不允许为空")]
    public string NewPassword { get; set; }
}