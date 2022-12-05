using BOOKSTore.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace BOOKSTore.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BOOKSToreEntityFrameworkCoreModule),
    typeof(BOOKSToreApplicationContractsModule)
    )]
public class BOOKSToreDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
