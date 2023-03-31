namespace Dotnet9.EShop.Service.Catalog.Services
{
    public class HealthService : ServiceBase
    {
        public IResult Get() => Results.Ok("success");
    }
}