using Dotnet9.EShop.Contracts.Catalog.Dto;
using Dotnet9.EShop.Contracts.Catalog.Request;
using Masa.Utils.Models;

namespace Dotnet9.EShop.Service.Catalog.Application.Catalogs.Queries;

public record CatalogItemsQuery : ItemsQueryBase<PaginatedListBase<CatalogListItemDto>>
{
    public string? Name { get; set; }

    public override PaginatedListBase<CatalogListItemDto> Result { get; set; } = default!;
}