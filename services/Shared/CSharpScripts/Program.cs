using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var hostBuilder = Host.CreateDefaultBuilder(args)
    .ConfigureSilkyGeneralHostDefaults()
    .UseNacosConfig("NacosConfig", Nacos.YamlParser.YamlConfigurationStringParser.Instance)
    .Build();
await hostBuilder.RunAsync();