using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Katameros.Repositories;

namespace Katameros
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string Origins = "_Origins";

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(Origins, builder =>
                {
#if DEBUG
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
#else
                    builder.WithOrigins("https://katameros.app", "https://katameros.netlify.app").AllowAnyHeader().AllowAnyMethod();
#endif
                });
            });
            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<LectionaryRepository>();
            services.AddScoped<ReadingsHelper>();
            services.AddScoped<ReadingsRepository>();
            services.AddScoped<FeastsFactory>();

            services.AddControllers();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(Origins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
