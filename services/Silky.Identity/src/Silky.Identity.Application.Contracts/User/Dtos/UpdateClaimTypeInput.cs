namespace Silky.Identity.Application.Contracts.User.Dtos;

public class UpdateClaimTypeInput
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 声明类型
    /// </summary>
    public string ClaimType { get; set; }

    /// <summary>
    /// 声明类型的值
    /// </summary>
    public string ClaimValue { get; set; }
}