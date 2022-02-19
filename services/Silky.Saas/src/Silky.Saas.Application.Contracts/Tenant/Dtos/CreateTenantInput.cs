using System.ComponentModel.DataAnnotations;
using Silky.Identity.Domain.Shared;
using Silky.Rpc.Auditing;

namespace Silky.Saas.Application.Contracts.Tenant.Dtos;

public class CreateTenantInput : TenantDtoBase
{
    [Required(ErrorMessage = "用户名不允许为空")]
    [MaxLength(50, ErrorMessage = "用户名不允许超过50个字符")]
    [RegularExpression(RegularExpressionConsts.UserName, ErrorMessage = "用户名格式不正确")]
    public string SuperUserName { get; set; }

    [Required(ErrorMessage = "电子邮箱不允许为空")]
    [EmailAddress(ErrorMessage = "邮箱地址格式不正确")]
    public string SuperUserEmail { get; set; }
    
    [RegularExpression(RegularExpressionConsts.Password, ErrorMessage = "密码格式不正确")]
    [DisableAuditing]
    public string SuperPassword { get; set; }

    [Required(ErrorMessage = "角色标识不允许为空")]
    [RegularExpression(RegularExpressionConsts.RoleName,ErrorMessage = "角色标识格式不正确")]
    public string SuperRoleName { get; set; }
    
    [Required(ErrorMessage = "角色真实名称不允许为空")]
    public string SuperRoleRealName { get; set; }
    
    [Required(ErrorMessage = "手机号码不允许为空")]
    [RegularExpression(RegularExpressionConsts.MobilePhone, ErrorMessage = "手机格式不正确")]
    public string SuperUserMobilePhone { get; set; }
    
    [Required(ErrorMessage = "工号不允许为空")]
    [RegularExpression(RegularExpressionConsts.JobNumber, ErrorMessage = "工号格式不正确")]
    public string SuperUserJobNumber { get; set; }
    
    [Required(ErrorMessage = "真实姓名不允许为空")]
    [MaxLength(50, ErrorMessage = "真实姓名不允许超过50个字符")]
    public string SuperRealName { get; set; }
}