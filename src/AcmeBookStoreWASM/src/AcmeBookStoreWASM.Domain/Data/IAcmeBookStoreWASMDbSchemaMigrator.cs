using System.Threading.Tasks;

namespace AcmeBookStoreWASM.Data
{
    public interface IAcmeBookStoreWASMDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
