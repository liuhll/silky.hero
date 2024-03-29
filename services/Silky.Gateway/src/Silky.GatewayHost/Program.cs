﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Silky.GatewayHost;


var hostBuilder = Host.CreateDefaultBuilder(args)
    .ConfigureSilkyGatewayDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
    .UseNacosConfig("NacosConfig", Nacos.YamlParser.YamlConfigurationStringParser.Instance)
    .Build();
await hostBuilder.RunAsync();