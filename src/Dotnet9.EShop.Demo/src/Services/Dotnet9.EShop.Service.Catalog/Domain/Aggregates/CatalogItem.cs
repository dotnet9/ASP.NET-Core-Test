namespace Dotnet9.EShop.Service.Catalog.Domain.Aggregates;

public class CatalogItem : FullAggregateRoot<Guid, int>
{
    public string Name { get; private set; } = default!;

    public string Description { get; private set; } = default!;

    public decimal Price { get; private set; } = default!;

    public string PictureFileName { get; private set; } = default!;

    private int _catalogTypeId;

    public CatalogType CatalogType { get; private set; } = default!;

    private Guid _catalogBrandId;

    public CatalogBrand CatalogBrand { get; private set; } = default!;

    public int AvailableStock { get; private set; }

    public int RestockThreshold { get; private set; }

    public CatalogItem(Guid id, Guid catalogBrandId, int catalogTypeId, string name, string description,
        decimal price, string pictureFileName) : base(id)
    {
        _catalogBrandId = catalogBrandId;

        _catalogTypeId = catalogTypeId;
        Name = name;
        Description = description;
        Price = price;
        PictureFileName = pictureFileName;
        AddCatalogItemDomainEvent();
    }

    private void AddCatalogItemDomainEvent()
    {
        var domainEvent = this.Map<CatalogItemCreatedIntegrationDomainEvent>();
        domainEvent.CatalogBrandId = _catalogBrandId;
        domainEvent.CatalogTypeId = _catalogTypeId;
        AddDomainEvent(domainEvent);
    }
}