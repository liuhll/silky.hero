using System.ComponentModel.DataAnnotations;
using Silky.Hero.Common;
using Silky.Rpc.Auditing;

namespace Silky.Identity.Application.Contracts.User.Dtos;

public class ChangePasswordInput
{
    /// <summary>
    /// 新密码
    /// </summary>
    [RegularExpression(RegularExpressionConsts.Password, ErrorMessage = "密码格式不正确")]
    [Required(ErrorMessage = "密码不允许为空")]
    [DisableAuditing]
    public string NewPassword { get; set; }
}