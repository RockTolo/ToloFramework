using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using Tolo.HttpClientLogger;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HttpClientLoggerServiceCollectionExtensions
    {
        public static IServiceCollection AddHttpClientLogger(
            this IServiceCollection services, 
            bool removeAllLoggers = true, 
            Func<string, bool>? shouldRedactHeaderValue = null)
        {
            shouldRedactHeaderValue ??= (header) => false;
            services.ConfigureHttpClientDefaults(builder =>
            {
                if (removeAllLoggers) 
                {
                    builder.RemoveAllLoggers();
                }
                
                builder.RedactLoggedHeaders(shouldRedactHeaderValue);
            });          
            InternalHttpClientLogger(services);
            return services;
        }

        public static IServiceCollection AddHttpClientLogger(
            this IServiceCollection services,
            bool removeAllLoggers,
            IEnumerable<string> redactedLoggedHeaderNames)
        {
            services.ConfigureHttpClientDefaults(builder =>
            {
                if (removeAllLoggers)
                {
                    builder.RemoveAllLoggers();
                }

                builder.RedactLoggedHeaders(redactedLoggedHeaderNames);
            });
            InternalHttpClientLogger(services);
            return services;
        }

        internal static IServiceCollection InternalHttpClientLogger(this IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IHttpMessageHandlerBuilderFilter, CustomHttpClientLoggerHandlerBuilderFilter>());
            services.TryAddTransient<ICustomHttpClientLogger, DefaultHttpClientLogger>();
            return services;
        }

        public static IHttpClientBuilder ConfigureHttpClientLogOptions(this IHttpClientBuilder builder, Action<HttpClientLogOptions> configureOptions)
        {
            builder.Services.Configure<HttpClientLogOptions>(builder.Name, configureOptions);
            return builder;
        }
    }
}
