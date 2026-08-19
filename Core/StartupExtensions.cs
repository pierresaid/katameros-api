using Katameros.Factories;
using Katameros.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Katameros;

public static class StartupExtensions
{
    public static IServiceCollection AddKatameros(this IServiceCollection services, string customPath = null)
    {
        services.AddScoped<LectionaryRepository>();
        services.AddScoped<FeastsRepository>();
        services.AddScoped<FastsRepository>();
        services.AddScoped<LectionaryRepository>();
        services.AddScoped<ReadingsHelper>();
        services.AddScoped<ReadingsRepository>();
        services.AddScoped<FeastsFactory>();
        services.AddScoped<FastsFactory>();
        services.AddScoped<SpecialCaseFactory>();

        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (customPath != null)
        {
            path = customPath;
        }
        services.AddDbContext<DatabaseContext>(options =>
        {
            // ;Pooling=false in Debug to add data (and drop Mode=ReadOnly).
            // ReadOnly keeps SQLite from leaving -wal/-shm lock files next to the
            // bundled database, which corrupt the fresh copy on the next deployment.
            var localFileConnectionString = $"Data Source={path}/KatamerosDatabase.db;Mode=ReadOnly";
            options.UseSqlite(localFileConnectionString);
        });

        return services;
    }
}
