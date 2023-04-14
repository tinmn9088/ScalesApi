using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/api/scales/weight", ([FromQuery] string id, [FromServices] ILogger<Program> logger) =>
{
    logger.LogInformation($"Request with id \"{id}\"");
    return new
    { 
        Weight = Math.Abs(Math.Floor((double)id.GetHashCode() % 10_000)) * 0.01,
        Unit = "Кг",
        Timestamp = DateTime.Now
    };
});

// Запуск приложения
app.Run();
