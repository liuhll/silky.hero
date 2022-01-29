using Silky.Hero.Common.Dtos;

namespace Silky.Identity.Application.Contracts.User.Dtos;

public class GetAddOrganizationUserPageInput : PageDtoBase
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    public string RealName { get; set; }
}