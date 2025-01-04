using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.Timing;

namespace Tolo.HttpClientLogger
{
    [DependsOn(typeof(AbpTimingModule))]
    public class HttpClientLoggerModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientLogger();
        }
    }
}
