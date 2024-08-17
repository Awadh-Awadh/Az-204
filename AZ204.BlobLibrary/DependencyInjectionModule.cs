using AZ2_4.BlobLibrary.Interfaces;
using AZ2_4.BlobLibrary.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AZ2_4.BlobLibrary;

public static class DependencyInjectionModule
{
    public static IServiceCollection Register(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IBlobClientService, BlobClientService>();

        return services;
    }
}