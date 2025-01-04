using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Options;

namespace Tolo.HttpClientLogger
{
    public class CustomHttpClientLoggerHandlerBuilderFilter : IHttpMessageHandlerBuilderFilter
    {
        private readonly IOptionsMonitor<HttpClientFactoryOptions> _optionsMonitor;
        private readonly IOptionsMonitor<HttpClientLogOptions> _optionsLogMoitor;
        private readonly IServiceProvider _serviceProvider;

        public CustomHttpClientLoggerHandlerBuilderFilter(
            IOptionsMonitor<HttpClientFactoryOptions> optionsMonitor, 
            IServiceProvider serviceProvider,
            IOptionsMonitor<HttpClientLogOptions> optionsLogMoitor)
        {
            _optionsMonitor = optionsMonitor;
            _serviceProvider = serviceProvider;
            _optionsLogMoitor = optionsLogMoitor;
        }

        public Action<HttpMessageHandlerBuilder> Configure(Action<HttpMessageHandlerBuilder> next)
        {
            return (builder) =>
            {
                next(builder);

                var factoryOptions = _optionsMonitor.Get(builder.Name);
                var logOptions = _optionsLogMoitor.Get(builder.Name);
                var customLogger = _serviceProvider.GetService<ICustomHttpClientLogger>();
                string clientName = !string.IsNullOrEmpty(builder.Name) ? builder.Name! : "Default";

                builder.AdditionalHandlers.Add(new CustomHttpClientLoggerHandler(
                    customLogger, factoryOptions, logOptions, clientName));
            };
        }
    }
}
