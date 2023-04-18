using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using ScalesApi.Contracts;
using ScalesApi.Services;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов в контейнер для внедрения зависимостей
builder.Services.Configure<SerialPortServiceConfiguration>(builder.Configuration.GetSection("SerialPort"));
builder.Services.AddScoped<ISerialPortService, SerialPortService>();
builder.Services.AddScoped<IScalesService, ScalesService>();
    
var app = builder.Build();

// Определение маршрутов API
app.MapGet("/api/scales/weight", ([FromQuery] string? id, ILogger<Program> logger, IScalesService scalesService) =>
{
    logger.LogInformation("Request with id \"{id}\"", id);
    return scalesService.GetWeight();
});

// Обработка исключений
app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        string? message = context.Features.Get<IExceptionHandlerFeature>()?.Error?.Message;
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = Application.Json;
        await context.Response.WriteAsJsonAsync(new { Message = message });
    });
});

// Запуск приложения
app.Run();
