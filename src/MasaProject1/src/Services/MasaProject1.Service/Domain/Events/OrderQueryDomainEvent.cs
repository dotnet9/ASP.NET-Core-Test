namespace MasaProject1.Service.Domain.Events;

public record OrderQueryDomainEvent : DomainEvent
{
    public List<Order> Orders { get; set; } = new();
}