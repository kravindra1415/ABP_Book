using System;
using System.Collections.Generic;
using System.Text;
using BOOKSTore.Localization;
using Volo.Abp.Application.Services;

namespace BOOKSTore;

/* Inherit your application services from this class.
 */
public abstract class BOOKSToreAppService : ApplicationService
{
    protected BOOKSToreAppService()
    {
        LocalizationResource = typeof(BOOKSToreResource);
    }
}
