using System.Threading.Tasks;

namespace BOOKSTore.Data;

public interface IBOOKSToreDbSchemaMigrator
{
    Task MigrateAsync();
}
