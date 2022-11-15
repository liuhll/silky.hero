using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Silky.Hero.Common;
using Silky.Hero.Common.Enums;
using Silky.Identity.Domain.Shared;
using Silky.Rpc.Auditing;

namespace Silky.Identity.Application.Contracts.User.Dtos;

public abstract class UserDtoBase
{
    
    /// <summary>
    /// 用户名
    /// </summary>
    [Required(ErrorMessage = "用户名不允许为空")]
    [MaxLength(50, ErrorMessage = "用户名不允许超过50个字符")]
    [RegularExpression(RegularExpressionConsts.UserName, ErrorMessage = "用户名格式不正确")]
    public string UserName { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    [Required(ErrorMessage = "真实姓名不允许为空")]
    [MaxLength(50, ErrorMessage = "真实姓名不允许超过50个字符")]
    public string RealName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [RegularExpression(RegularExpressionConsts.Password, ErrorMessage = "密码格式不正确")]
    [DisableAuditing]
    public string Password { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string Surname { get; set; }

    /// <summary>
    /// 生日
    /// </summary>
    public DateTime? BirthDay { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public Sex? Sex { get; set; }

    /// <summary>
    /// 电子邮件
    /// </summary>
    [Required(ErrorMessage = "电子邮箱不允许为空")]
    [EmailAddress(ErrorMessage = "邮箱地址格式不正确")]
    public string Email { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    [RegularExpression(RegularExpressionConsts.TelPhone, ErrorMessage = "电话格式不正确")]
    public string TelPhone { get; set; }

    /// <summary>
    /// 手机
    /// </summary>
    [Required(ErrorMessage = "手机号码不允许为空")]
    [RegularExpression(RegularExpressionConsts.MobilePhone, ErrorMessage = "手机格式不正确")]
    public string MobilePhone { get; set; }

    /// <summary>
    /// 工号
    /// </summary>
    [Required(ErrorMessage = "工号不允许为空")]
    [RegularExpression(RegularExpressionConsts.JobNumber, ErrorMessage = "工号格式不正确")]
    public string JobNumber { get; set; }
    
    /// <summary>
    /// 用户状态
    /// </summary>
    public Status Status { get; set; }

    /// <summary>
    /// 是否支持锁定
    /// </summary>
    public bool LockoutEnabled { get; set; }

    /// <summary>
    /// 用户岗位、组织机构信息
    /// </summary>
    public virtual ICollection<UserSubsidiaryDto> UserSubsidiaries { get; set; }
    
}