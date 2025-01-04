namespace Tolo.HttpClientLogger
{
    public interface ICustomHttpClientLogger
    {
        ValueTask LogRequestStartAsync(
            LogContext context,
            CancellationToken cancellationToken = default);

        ValueTask LogRequestStopAsync(
            LogContext context,
            CancellationToken cancellationToken = default);

        ValueTask LogRequestFailedAsync(
            LogContext context,
            CancellationToken cancellationToken = default);
    }
}
