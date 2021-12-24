using System;
using Silky.Identity.Domain.Shared;

namespace Silky.Account.Application.Contracts.Account.Dtos;

public class GetCurrentUserInfoOutput
{
    /// <summary>
    /// 用户id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    public string RealName { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string Surname { get; set; }

    /// <summary>
    /// 生日
    /// </summary>
    public DateTime BirthDay { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public Sex? Sex { get; set; }

    /// <summary>
    /// 电子邮件
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// 联系电话
    /// </summary>
    public string TelPhone { get; set; }

    /// <summary>
    /// 手机
    /// </summary>
    public string MobilePhone { get; set; }
}