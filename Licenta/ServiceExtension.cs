using Licenta.Core.Interfaces;
using Licenta.External.CSV;
using Licenta.External.ML;
using Licenta.External.SendGrid;
using Licenta.Infrastructure.Repository;
using Licenta.Infrastructure.Seeders;
using Licenta.Services.Helpers;
using Licenta.Services.Interfaces;
using Licenta.Services.Interfaces.External;
using Licenta.Services.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Licenta.Api;

public static class ServiceExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddTransient<IPartnerManager, PartnerManager>();
        services.AddTransient<IJobManager, JobManager>();
        services.AddTransient<IFileManager, FileManager>();
        services.AddTransient<IStudentManager, StudentManager>();
        services.AddTransient<IEventManager, EventManager>();
        services.AddTransient<IPracticeManager, PracticeManager>();

        services.AddTransient<IAuthManager, AuthManager>();
        services.AddTransient<ITokenHelper, TokenHelper>();
        services.AddTransient<ISendgridManager, SendgridManager>();
        services.AddTransient<ICSVService, CSVService>();
        services.AddTransient<IModelService, ModelService>();
        services.AddTransient<DataSeeder>();
    }
}
