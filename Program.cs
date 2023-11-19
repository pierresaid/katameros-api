using Katameros;
using Katameros.Factories;
using Katameros.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("_Origins", builder =>
    {
#if DEBUG
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
#else
        builder.WithOrigins("https://katameros.app", "https://katameros.netlify.app", "https://new-katameros.netlify.app", "https://copte.fr").AllowAnyHeader().AllowAnyMethod();
#endif
    });
});

builder.Services.AddScoped<LectionaryRepository>();
builder.Services.AddScoped<FeastsRepository>();
builder.Services.AddScoped<LectionaryRepository>();
builder.Services.AddScoped<ReadingsHelper>();
builder.Services.AddScoped<ReadingsRepository>();
builder.Services.AddScoped<FeastsFactory>();
builder.Services.AddScoped<SpecialCaseFactory>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("_Origins");

app.MapControllers();

app.Run();
