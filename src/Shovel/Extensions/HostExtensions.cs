using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Shovel.Extensions;

public static class HostExtensions
{
    public static HostApplicationBuilder ConfigureServices(this HostApplicationBuilder builder,
        Action<IConfigurationManager, IServiceCollection> configure)
    {
        configure(builder.Configuration, builder.Services);
        return builder;
    }
}