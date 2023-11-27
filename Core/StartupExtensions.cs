using Katameros.Factories;
using Katameros.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Katameros;

public static class StartupExtensions
{
    public static IServiceCollection AddKatameros(this IServiceCollection services)
    {
        services.AddScoped<LectionaryRepository>();
        services.AddScoped<FeastsRepository>();
        services.AddScoped<LectionaryRepository>();
        services.AddScoped<ReadingsHelper>();
        services.AddScoped<ReadingsRepository>();
        services.AddScoped<FeastsFactory>();
        services.AddScoped<SpecialCaseFactory>();

        var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        services.AddDbContext<DatabaseContext>(options =>
        {
            var localFileConnectionString = $"Data Source={path}/KatamerosDatabase.db";
            options.UseSqlite(localFileConnectionString);
        });

        return services;
    }
}
