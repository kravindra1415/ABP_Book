using Volo.Abp.Settings;

namespace BOOKSTore.Settings;

public class BOOKSToreSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(BOOKSToreSettings.MySetting1));
    }
}
