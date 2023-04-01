using System.Reflection;
using Dotnet9.EShop.Service.Catalog.Infrastructure;
using Dotnet9.EShop.Service.Catalog.Infrastructure.Middleware;
using FluentValidation;
using Masa.BuildingBlocks.Data.UoW;
using Masa.BuildingBlocks.Dispatcher.Events;
using Masa.BuildingBlocks.Dispatcher.IntegrationEvents;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMapster().AddMasaDbContext<CatalogDbContext>(dbContextBuilder =>
    {
        dbContextBuilder.UseSqlite().UseFilter();
    })
    .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()) // ���ָ�������µ�`FluentValidation`��֤��
    .AddDomainEventBus(options =>
    {
        options.UseIntegrationEventBus(integrationEventBus =>
                integrationEventBus
                    .UseDapr()
                    .UseEventLog<CatalogDbContext>())
            .UseEventBus(eventBusBuilder =>
                eventBusBuilder.UseMiddleware(typeof(LoggingEventMiddleware<>))) // ָ����Ҫִ�е��м��
            .UseUoW<CatalogDbContext>() // ʹ�ù�����Ԫ��ȷ��ԭ����
            .UseRepository<CatalogDbContext>();
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.AddServices();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}