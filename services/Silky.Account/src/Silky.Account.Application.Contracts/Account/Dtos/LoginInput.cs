using System.ComponentModel.DataAnnotations;
using Silky.Rpc.Auditing;

namespace Silky.Account.Application.Contracts.Account.Dtos;

public class LoginInput
{
    /// <summary>
    /// 账号
    /// </summary>
    [Required(ErrorMessage = "账号不允许为空")] public string Account { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [Required(ErrorMessage = "密码不允许为空")]
    [DisableAuditing]
    public string Password { get; set; }

    /// <summary>
    /// 所属租户
    /// </summary>
    public long? TenantId { get; set; }
}