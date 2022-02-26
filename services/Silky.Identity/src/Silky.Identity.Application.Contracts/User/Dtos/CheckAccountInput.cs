using System.ComponentModel.DataAnnotations;
using Silky.Identity.Domain.Shared;

namespace Silky.Identity.Application.Contracts.User.Dtos;

public class CheckAccountInput
{
    /// <summary>
    /// 账号Id
    /// </summary>
    public long? Id { get; set; }
    
    /// <summary>
    /// 账号
    /// </summary>
    [Required(ErrorMessage = "账号不允许为空")]
    public string Account { get; set; }

    /// <summary>
    /// 账号类型
    /// </summary>
    public AccountType AccountType { get; set; }
}