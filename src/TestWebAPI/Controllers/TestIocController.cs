namespace TestWebAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class TestIocController : ControllerBase
{
    private readonly IServiceProvider _serviceProvider;

    public TestIocController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    [HttpGet]
    public async Task<string> Transient([FromServices] TransientService service)
    {
        Console.WriteLine($"1. 注入服务Id： {service.Id}");

        var innerService1 = _serviceProvider.GetService<TransientService>();
        Console.WriteLine($"2. 方法内获取的服务Id： {innerService1!.Id}");

        var innerService2 = _serviceProvider.GetService<TransientService>();
        Console.WriteLine($"3. 方法内获取的服务Id： {innerService2!.Id}");

        return service.Id;
    }

    [HttpGet]
    public async Task<string> Scoped([FromServices] ScopedService service)
    {
        Console.WriteLine($"1. 注入服务Id： {service.Id}");

        var innerService1 = _serviceProvider.GetService<ScopedService>();
        Console.WriteLine($"2. 方法内获取的服务Id： {innerService1!.Id}");

        var innerService2 = _serviceProvider.GetService<ScopedService>();
        Console.WriteLine($"3. 方法内获取的服务Id： {innerService2!.Id}");

        return service.Id;
    }

    [HttpGet]
    public async Task<string> Singleton([FromServices] SingletonService service)
    {
        Console.WriteLine($"1. 注入服务Id： {service.Id}");

        var innerService1 = _serviceProvider.GetService<SingletonService>();
        Console.WriteLine($"2. 方法内获取的服务Id： {innerService1!.Id}");

        var innerService2 = _serviceProvider.GetService<SingletonService>();
        Console.WriteLine($"3. 方法内获取的服务Id： {innerService2!.Id}");

        return service.Id;
    }
}