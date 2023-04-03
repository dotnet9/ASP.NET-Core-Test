using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Queries;

namespace Dotnet9.EShop.Contracts.Catalog.Request;

public abstract record ItemsQueryBase<TResult> : Query<TResult>
{
    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 20;
}