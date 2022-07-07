namespace TestWebAPI.Services;

public record TransientService
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
}