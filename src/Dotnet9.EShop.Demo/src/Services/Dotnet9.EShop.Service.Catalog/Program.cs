var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddMapster()
    .AddSequentialGuidGenerator()
    .AddMasaDbContext<CatalogDbContext>(dbContextBuilder =>
    {
        dbContextBuilder
            .UseSqlite()
            .UseFilter();
    })
    .AddMultilevelCache(distributedCacheOptions => { distributedCacheOptions.UseStackExchangeRedisCache(); })
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
app.UseMasaExceptionHandler();

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