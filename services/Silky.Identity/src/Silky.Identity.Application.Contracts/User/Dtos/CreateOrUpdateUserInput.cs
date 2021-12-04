using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Silky.Identity.Domain.Shared;
using Silky.Rpc.CachingInterceptor;

namespace Silky.Identity.Application.Contracts.User.Dtos;

public class CreateOrUpdateUserInput
{
    [CacheKey(0)]
    public long? Id { get; set; }

    [Required(ErrorMessage = "用户名不允许为空")]
    [MaxLength(50, ErrorMessage = "用户名不允许超过50个字符")]
    [NotNull]
    public string UserName { get; set; }

    [Required(ErrorMessage = "真实姓名不允许为空")]
    [MaxLength(50, ErrorMessage = "真实姓名不允许超过50个字符")]
    [NotNull]
    public string RealName { get; set; }
    
    [RegularExpression(RegularExpressionConsts.Password, ErrorMessage = "密码格式不正确")]
    [NotNull]
    public string Password { get; set; }

    public string Surname { get; set; }

    public DateTime BirthDay { get; set; }

    public Sex? Sex { get; set; }

    [Required(ErrorMessage = "电子邮箱不允许为空")]
    [EmailAddress(ErrorMessage = "邮箱地址格式不正确")]
    [NotNull]
    public string Email { get; set; }

    [RegularExpression(RegularExpressionConsts.TelPhone, ErrorMessage = "电话格式不正确")]
    public string TelPhone { get; set; }

    [Required(ErrorMessage = "手机号码不允许为空")]
    [RegularExpression(RegularExpressionConsts.MobilePhone, ErrorMessage = "手机格式不正确")]
    [NotNull]
    public string MobilePhone { get; set; }
    
    public string JobNumber { get; set; }

    public bool LockoutEnabled { get; set; }
    
    public ICollection<UserSubsidiaryDto> UserSubsidiaries { get; set; }
  
}