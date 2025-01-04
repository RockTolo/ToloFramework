namespace Tolo.HttpClientLogger
{
    /// <summary>
    /// 请求消息存储接口
    /// </summary>
    public interface IHttpClientLogStore
    {
        Task SaveAsync(LogContext context, HttpClientLogInfo logInfo, CancellationToken cancellationToken);
    }
}
