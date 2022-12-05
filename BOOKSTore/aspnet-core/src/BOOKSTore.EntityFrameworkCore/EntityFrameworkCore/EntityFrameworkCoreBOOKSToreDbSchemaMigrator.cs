using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BOOKSTore.Data;
using Volo.Abp.DependencyInjection;

namespace BOOKSTore.EntityFrameworkCore;

public class EntityFrameworkCoreBOOKSToreDbSchemaMigrator
    : IBOOKSToreDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreBOOKSToreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the BOOKSToreDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<BOOKSToreDbContext>()
            .Database
            .MigrateAsync();
    }
}
