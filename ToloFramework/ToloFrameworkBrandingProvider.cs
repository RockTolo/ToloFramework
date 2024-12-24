using Microsoft.Extensions.Localization;
using ToloFramework.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace ToloFramework;

[Dependency(ReplaceServices = true)]
public class ToloFrameworkBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<ToloFrameworkResource> _localizer;

    public ToloFrameworkBrandingProvider(IStringLocalizer<ToloFrameworkResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}