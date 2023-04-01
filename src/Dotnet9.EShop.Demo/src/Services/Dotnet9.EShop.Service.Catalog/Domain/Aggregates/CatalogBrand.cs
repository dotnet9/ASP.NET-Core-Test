namespace Dotnet9.EShop.Service.Catalog.Domain.Aggregates;

public class CatalogBrand : FullAggregateRoot<Guid, int>
{
    public string Brand { get; private set; } = null!;

    public CatalogBrand(string brand)
    {
        Brand = brand;
    }
}