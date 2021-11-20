using Silky.Rpc.Routing;
using Silky.Rpc.Security;
using Silky.Organization.Application.Contracts.System.Dtos;

namespace Silky.Organization.Application.Contracts.System
{
    /// <summary>
    /// 系统信息服务
    /// </summary>
    [ServiceRoute(template:"api/system/{appservice=silky.organization}")]
    public interface ISystemAppService
    {
        /// <summary>
        /// 获取当前应用详细信息
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        GetSystemInfoOutput GetInfo();
    }
}