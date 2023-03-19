using Licenta.Core.Interfaces;
using Licenta.Infrastructure.Repository;
using Licenta.Infrastructure.Seeders;
using Licenta.Services.Helpers;
using Licenta.Services.Interfaces;
using Licenta.Services.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Licenta.Api;

public static class ServiceExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddTransient<IPartnerManager, PartnerManager>();
        services.AddTransient<IAuthManager, AuthManager>();
        services.AddTransient<ITokenHelper, TokenHelper>();
        services.AddTransient<DataSeeder>();
    }
}
