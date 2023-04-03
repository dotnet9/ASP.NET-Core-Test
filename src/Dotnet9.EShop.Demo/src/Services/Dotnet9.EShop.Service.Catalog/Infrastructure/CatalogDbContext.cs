namespace Dotnet9.EShop.Service.Catalog.Infrastructure;

public class CatalogDbContext : MasaDbContext<CatalogDbContext>
{
    public CatalogDbContext(MasaDbContextOptions<CatalogDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreatingExecuting(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
        base.OnModelCreatingExecuting(modelBuilder);
    }
}