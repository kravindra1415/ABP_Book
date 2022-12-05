using BOOKSTore.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace BOOKSTore.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class BOOKSToreController : AbpControllerBase
{
    protected BOOKSToreController()
    {
        LocalizationResource = typeof(BOOKSToreResource);
    }
}
