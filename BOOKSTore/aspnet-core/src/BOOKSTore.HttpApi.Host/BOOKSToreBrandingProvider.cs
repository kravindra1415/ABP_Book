using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace BOOKSTore;

[Dependency(ReplaceServices = true)]
public class BOOKSToreBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "BOOKSTore";
}
