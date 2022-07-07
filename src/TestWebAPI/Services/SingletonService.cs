namespace TestWebAPI.Services;

public record SingletonService
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
}