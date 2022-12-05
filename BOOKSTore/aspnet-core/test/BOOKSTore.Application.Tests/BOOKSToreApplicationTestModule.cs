using Volo.Abp.Modularity;

namespace BOOKSTore;

[DependsOn(
    typeof(BOOKSToreApplicationModule),
    typeof(BOOKSToreDomainTestModule)
    )]
public class BOOKSToreApplicationTestModule : AbpModule
{

}
