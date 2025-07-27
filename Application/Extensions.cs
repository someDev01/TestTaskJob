using Application.Services.Platforms;
using Application.Services.WriteFile;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Extensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IWriteFileService, WriteFileService>();
        services.AddScoped<IPlatformService, PlatformService>();
    }
}