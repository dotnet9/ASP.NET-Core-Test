namespace MasaProject1.Service.Actors;

public interface IOrderActor : IActor
{
    Task<List<Order>> GetListAsync();
}