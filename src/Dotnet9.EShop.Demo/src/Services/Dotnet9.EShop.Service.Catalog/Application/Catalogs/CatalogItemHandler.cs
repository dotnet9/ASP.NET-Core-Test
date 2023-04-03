namespace Dotnet9.EShop.Service.Catalog.Application.Catalogs
{
    public class CatalogItemHandler
    {
        private readonly ICatalogItemRepository _catalogItemRepository;

        public CatalogItemHandler(ICatalogItemRepository catalogItemRepository)
        {
            _catalogItemRepository = catalogItemRepository;
        }

        /// <summary>
        /// 创建商品处理程序
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [EventHandler]
        public async Task AddAsync(CatalogItemCommand command, ISequentialGuidGenerator guidGenerator,
            CancellationToken cancellationToken)
        {
            var catalogItem = new CatalogItem(guidGenerator.NewId(), command.CatalogBrandId, command.CatalogTypeId,
                command.Name,
                command.Description, command.Price, command.PictureFileName);
            await _catalogItemRepository.AddAsync(catalogItem, cancellationToken);
        }

        [EventHandler]
        public async Task GetListAsync(CatalogItemsQuery query, CancellationToken cancellationToken)
        {
            Expression<Func<CatalogItem, bool>> condition = catalogItem => true;
            condition = condition.And(!query.Name.IsNullOrWhiteSpace(),
                catalogItem => catalogItem.Name.Contains(query.Name!));

            var catalogItems = await _catalogItemRepository.GetPaginatedListAsync(condition,
                new PaginatedOptions(query.Page, query.PageSize), cancellationToken);

            query.Result = new PaginatedListBase<CatalogListItemDto>()
            {
                Total = catalogItems.Total,
                TotalPages = catalogItems.TotalPages,
                Result = catalogItems.Result.Map<List<CatalogListItemDto>>()
            };
        }
    }
}