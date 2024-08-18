using API.Services;
using Katameros;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var telegramBotKey = builder.Configuration["TelegramBotKey"];

builder.Services.AddHttpClient("telegram", c =>
{
    c.BaseAddress = new Uri($"https://api.telegram.org/{telegramBotKey}/");
});

builder.Services.AddScoped<NotificationService>();


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

builder.Services.AddKatameros();

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
