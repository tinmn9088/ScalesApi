using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Diagnostics;
using ScalesApi.Contracts;
using ScalesApi.Services;
using ScalesApi.Utils;

var builder = WebApplication.CreateBuilder(args);

// Добавление и настройка сервисов в контейнере для внедрения зависимостей
builder.Services.Configure<SerialPortServiceConfiguration>(builder.Configuration.GetSection("SerialPort"));
builder.Services.Configure<FileLoggerConfiguration>(builder.Configuration.GetSection("Logging").GetSection("File"));
builder.Services.AddScoped<ISerialPortService, SerialPortService>();
builder.Services.AddScoped<IScalesService, ScalesService>();
builder.Services.AddSingleton<ILoggerProvider, FileLoggerProvider>();
    
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
        app.Logger.LogError("{message}", message);
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = Application.Json;
        await context.Response.WriteAsJsonAsync(new { Message = message });
    });
});

// Запуск приложения
app.Run();
