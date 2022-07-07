namespace TestWebAPI.Services;

public record ScopedService
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
}