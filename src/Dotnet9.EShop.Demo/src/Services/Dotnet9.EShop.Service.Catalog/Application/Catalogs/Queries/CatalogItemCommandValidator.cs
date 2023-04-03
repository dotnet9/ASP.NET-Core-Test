namespace Dotnet9.EShop.Service.Catalog.Application.Catalogs.Queries;

public class CatalogItemCommandValidator:AbstractValidator<CatalogItemCommand>
{
    public CatalogItemCommandValidator()
    {
        RuleFor(command => command.Name).NotNull().Length(1, 20).WithMessage("商品名称长度介于在1-20之间");
        RuleFor(command => command.CatalogTypeId)
            .Must(typeId => Enumeration.GetAll<CatalogType>().Any(item => item.Id == typeId)).WithMessage("不支持的商品分类");
    }
}