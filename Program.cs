using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Diagnostics;
using ScalesApi.Contracts;
using ScalesApi.Services;
using NLog;
using NLog.Extensions.Logging;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Применение конфигурации для логирования
LogManager.Configuration = new NLogLoggingConfiguration(builder.Configuration.GetSection("NLog"));

// Добавление и настройка сервисов в контейнере для внедрения зависимостей
builder.Services.Configure<SerialPortServiceConfiguration>(builder.Configuration.GetSection("SerialPort"));
builder.Services.AddScoped<ISerialPortService, SerialPortService>();
builder.Services.AddScoped<IScalesService, ScalesService>();

Logger logger = LogManager.GetCurrentClassLogger();
WebApplication app = builder.Build();

// Определение маршрутов API
app.MapGet("/api/scales/weight", ([FromQuery] string? id, IScalesService scalesService) =>
{
    logger.Info($"Request with id \"{id}\"");

    WeightData weightData = scalesService.GetWeight();
    logger.Debug(weightData);

    return weightData;
});

// Обработка исключений
app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        Exception? error = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        string? message = error?.Message;
        logger.Error(error, message);
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = Application.Json;
        await context.Response.WriteAsJsonAsync(new { Message = message });
    });
});

// Запуск приложения
app.Run();
