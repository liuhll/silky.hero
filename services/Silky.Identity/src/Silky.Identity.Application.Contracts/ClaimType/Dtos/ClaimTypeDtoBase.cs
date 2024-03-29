﻿using System.ComponentModel.DataAnnotations;
using Silky.Identity.Domain.Shared;

namespace Silky.Identity.Application.Contracts.ClaimType.Dtos;

public abstract class ClaimTypeDtoBase
{
    /// <summary>
    /// 声明名称
    /// </summary>
    [Required(ErrorMessage = "声明名称不允许为空")]
    public string Name { get; set; }

    /// <summary>
    /// 是否必须
    /// </summary>
    public bool Required { get; set; }

    /// <summary>
    /// 是否静态类型
    /// </summary>
    public bool IsStatic { get; set; }

    /// <summary>
    /// 正则表达式
    /// </summary>
    public string Regex { get; set; }

    /// <summary>
    /// 正则表达式描述
    /// </summary>
    public string RegexDescription { get; set; }

    /// <summary>
    /// 声明描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 声明值类型
    /// </summary>
    public IdentityClaimValueType ValueType { get; set; }
}