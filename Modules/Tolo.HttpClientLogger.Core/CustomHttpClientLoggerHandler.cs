using Microsoft.Extensions.Http;

namespace Tolo.HttpClientLogger
{
    public class CustomHttpClientLoggerHandler : DelegatingHandler
    {
        private readonly ICustomHttpClientLogger? _httpClientLogger;
        private readonly HttpClientFactoryOptions _factoryOptions;
        private readonly HttpClientLogOptions _logOptions;
        private readonly string _clientName;

        public CustomHttpClientLoggerHandler(
            ICustomHttpClientLogger? httpClientLogger,
            HttpClientFactoryOptions factoryOptions,
            HttpClientLogOptions logOptions,
            string clientName)
        {
            _httpClientLogger = httpClientLogger;
            _factoryOptions = factoryOptions;
            _logOptions = logOptions;
            _clientName = clientName;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            var context = _httpClientLogger is null ? null : new LogContext(_clientName, request, _factoryOptions, _logOptions);
            var stopwatch = ValueStopwatch.StartNew();
            HttpResponseMessage? response = null;
            try
            {
                if (_httpClientLogger is not null)
                {
                    await _httpClientLogger.LogRequestStartAsync(context!, cancellationToken).ConfigureAwait(false);
                }

                response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

                if (_httpClientLogger is not null)
                {
                    context!.Response = response;
                    context.Elapsed = stopwatch.GetElapsedTime();
                    await _httpClientLogger.LogRequestStopAsync(context, cancellationToken).ConfigureAwait(false);
                }
                return response;
            }
            catch (Exception exception)
            {
                if (_httpClientLogger is not null)
                {
                    context!.Response = response;
                    context.Elapsed = stopwatch.GetElapsedTime();
                    context.Exception = exception;
                    await _httpClientLogger.LogRequestFailedAsync(context, cancellationToken).ConfigureAwait(false);
                }
                throw;
            }
        }
    }
}
