namespace Dotnet9.EShop.Service.Catalog.Domain.Events;

public record CatalogItemCreatedIntegrationDomainEvent : CatalogItemCreatedIntegrationEvent, IIntegrationDomainEvent
{
}