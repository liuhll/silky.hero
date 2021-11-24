using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Silky.Account.Domain.Shared;
using Silky.Account.Domain.Shared.Users;

namespace Silky.Account.Application.Contracts.User.Dtos;

public class CreateUserInput
{
    [Required(ErrorMessage = "用户名不允许为空")] public string UserName { get; set; }

    [Required(ErrorMessage = "真实姓名不允许为空")] public string RealName { get; set; }

    [Required(ErrorMessage = "密码不允许为空")]
    [RegularExpression(RegularExpressionConstant.Password, ErrorMessage = "密码格式不正确")]
    public string Password { get; set; }

    public string NickName { get; set; }

    public DateTime BirthDay { get; set; }

    public Sex? Sex { get; set; }

    [Required(ErrorMessage = "电子邮件不允许为空")] 
    [EmailAddress(ErrorMessage = "电子邮件格式不正确")]
    public string Email { get; set; }

   // [RegularExpression(RegularExpressionConstant.TelPhone, ErrorMessage = "联系电话格式不正确")]
    public string TelPhone { get; set; }

    [Required(ErrorMessage = "手机号码不允许为空")] 
    [RegularExpression(RegularExpressionConstant.MobilePhone, ErrorMessage = "手机号码格式不正确")]
    public string MobilePhone { get; set; }

    public long OrganizationId { get; set; }

    public long PositionId { get; set; }

    public string JobNumber { get; set; }

    public ICollection<CreateUserSubsidiaryInput> UserSubsidiaries { get; set; }
}