using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Tolo.HttpClientLogger
{
    [Dependency(TryRegister = true)]
    public class DefaultHttpClientLogStore : IHttpClientLogStore, ISingletonDependency
    {
        public ILogger<DefaultHttpClientLogStore> Logger { get; set; }

        public DefaultHttpClientLogStore()
        {
            Logger  = NullLogger<DefaultHttpClientLogStore>.Instance;
        }

        public Task SaveAsync(
            LogContext context, 
            HttpClientLogInfo logInfo, 
            CancellationToken cancellationToken)
        {
            Logger.LogWarning($"IHttpClientLogStore was not implemented! Using {nameof(DefaultHttpClientLogStore)}:");
            Logger.LogWarning(logInfo.ToString());

            return Task.CompletedTask;
        }
    }
}
