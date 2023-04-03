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
    .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()) // 添加指定程序集下的`FluentValidation`验证器
    .AddDomainEventBus(options =>
    {
        options.UseIntegrationEventBus(integrationEventBus =>
                integrationEventBus
                    .UseDapr()
                    .UseEventLog<CatalogDbContext>())
            .UseEventBus(eventBusBuilder =>
                eventBusBuilder.UseMiddleware(typeof(LoggingEventMiddleware<>))) // 指定需要执行的中间件
            .UseUoW<CatalogDbContext>() // 使用工作单元，确保原子性
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