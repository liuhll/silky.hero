using Silky.Rpc.Routing;
using Silky.Rpc.Security;
using Silky.Log.Application.Contracts.System.Dtos;

namespace Silky.Log.Application.Contracts.System
{
    /// <summary>
    /// 系统信息服务
    /// </summary>
    [ServiceRoute(template:"api/system/{appservice=silky.log}")]
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