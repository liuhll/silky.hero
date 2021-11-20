using System.Linq;
using Silky.Core;
using Silky.Rpc.Runtime.Server;
using Silky.Identity.Application.Contracts.System;
using Silky.Identity.Application.Contracts.System.Dtos;

namespace Silky.Identity.Application.System
{
    public class SystemAppService : ISystemAppService
    {
        private readonly IServerManager _serverManager;

        public SystemAppService(IServerManager serverManager)
        {
            _serverManager = serverManager;
        }        

        public GetSystemInfoOutput GetInfo()
        {
            return new GetSystemInfoOutput()
            {
                HostName = EngineContext.Current.HostName,
                Environment = EngineContext.Current.HostEnvironment.EnvironmentName,
                Addresses = _serverManager.GetSelfServer().Endpoints.Select(p => p.ToString()).ToArray()
            };
        }
    }
}