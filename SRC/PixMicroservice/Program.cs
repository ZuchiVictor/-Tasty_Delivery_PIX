using Google.Api;
using MercadoPago.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OWASPZAPDotNetAPI;
using PixMicroservice.EventHandlers;
using PixMicroservice.Sagas;
using RabbitMQ.Client;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPixContext, PixContext>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pix API", Version = "v1" });
});

// Configurar RabbitMQ
var factory = new ConnectionFactory() { HostName = "localhost" };
var connection = factory.CreateConnection();
var channel = connection.CreateModel();

channel.QueueDeclare(queue: "payment",
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);
channel.ConfirmSelect();

builder.Services.AddSingleton(channel);
builder.Services.AddSingleton<PaymentInitiatedEventHandler>();
builder.Services.AddSingleton<PaymentSaga>();




var app = builder.Build();



// Configure o pipeline HTTP.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var eventHandler = app.Services.GetService<PaymentInitiatedEventHandler>();
eventHandler.Start();

// Initialize SDK
MercadoPagoConfig.AccessToken = "TEST-4763904494948372-052122-142d8d655e8b9f22f5d241ad3612cf62-80196247";




app.UseSwagger();
    app.UseSwaggerUI();



//app.UseHttpsRedirection();



//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast =  Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();

app.Run();



record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
