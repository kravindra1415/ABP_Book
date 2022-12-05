using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace BOOKSTore.Data;

/* This is used if database provider does't define
 * IBOOKSToreDbSchemaMigrator implementation.
 */
public class NullBOOKSToreDbSchemaMigrator : IBOOKSToreDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
