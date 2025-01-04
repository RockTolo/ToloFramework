using Volo.Abp.DependencyInjection;

namespace Tolo.HttpClientLogger
{
    [Dependency(ReplaceServices = true)]
    public class CustomHttpClientLogger : DefaultHttpClientLogger, ITransientDependency
    {
        private readonly IHttpClientLogStore _logStore;

        public CustomHttpClientLogger(IHttpClientLogStore logStore)
        {
            _logStore = logStore;
        }

        public override async ValueTask LogRequestStopAsync(LogContext context, CancellationToken cancellationToken = default)
        {
            await base.LogRequestStopAsync(context, cancellationToken);
            await _logStore.SaveAsync(context, LogInfo!, cancellationToken);
        }

        public override async ValueTask LogRequestFailedAsync(LogContext context, CancellationToken cancellationToken = default)
        {
            await base.LogRequestFailedAsync(context, cancellationToken);
            await _logStore.SaveAsync(context, LogInfo!, cancellationToken);
        }
    }
}