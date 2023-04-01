using Dotnet9.EShop.Service.Catalog.Domain.Repositories;
using Masa.BuildingBlocks.Data.UoW;
using Masa.Contrib.Ddd.Domain.Repository.EFCore;

namespace Dotnet9.EShop.Service.Catalog.Infrastructure.Repositories;

public class CatalogItemRepository : Repository<CatalogDbContext, CatalogItem, Guid>, ICatalogItemRepository
{
    public CatalogItemRepository(CatalogDbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }
}