namespace Dotnet9.EShop.Service.Catalog.Application.Catalogs.Commands;

public record CatalogItemCommand : Command
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string PictureFileName { get; set; } = string.Empty;

    public Guid CatalogBrandId { get; set; }

    public int CatalogTypeId { get; set; }
}