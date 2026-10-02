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
    options.AddPolicy("_Origins", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
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
app.UseCors("_Origins");
app.UseAuthorization();
app.MapControllers();
app.Run();
