using BOOKSTore.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace BOOKSTore;

[DependsOn(
    typeof(BOOKSToreEntityFrameworkCoreTestModule)
    )]
public class BOOKSToreDomainTestModule : AbpModule
{

}
