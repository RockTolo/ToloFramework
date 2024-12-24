using Volo.Abp.Application.Services;
using ToloFramework.Localization;

namespace ToloFramework.Services;

/* Inherit your application services from this class. */
public abstract class ToloFrameworkAppService : ApplicationService
{
    protected ToloFrameworkAppService()
    {
        LocalizationResource = typeof(ToloFrameworkResource);
    }
}