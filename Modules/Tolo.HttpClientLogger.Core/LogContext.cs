using Microsoft.Extensions.Http;

namespace Tolo.HttpClientLogger
{
    public class LogContext
    {
        public string ClientName { get; protected internal set;}

        public HttpRequestMessage Request { get; protected internal set; }

        public HttpClientFactoryOptions HttpClientFactoryOptions { get; protected internal set; }

        public HttpClientLogOptions HttpClientLogOptions { get; protected internal set; }

        public HttpResponseMessage? Response { get; protected internal set; }

        public TimeSpan Elapsed { get; protected internal set; }

        public Exception? Exception { get; protected internal set; }

        public Dictionary<string, object> Properties { get; protected internal set; } = new();

        public LogContext(string clientName, 
            HttpRequestMessage httpRequestMessage, 
            HttpClientFactoryOptions httpClientFactoryOptions,
            HttpClientLogOptions httpClientLogOptions)
        {
            ClientName = clientName;
            Request = httpRequestMessage;
            HttpClientFactoryOptions = httpClientFactoryOptions;
            HttpClientLogOptions = httpClientLogOptions;
        }

        /// <summary>
        /// 根据key获取扩展属性value
        /// </summary>
        /// <typeparam name="TProperty">value类型</typeparam>
        /// <param name="key">key</param>
        /// <returns></returns>
        public virtual TProperty GetProperty<TProperty>(string key)
        {
            object valueObj;
            if (Properties.TryGetValue(key, out valueObj) && valueObj is TProperty)
            {
                return (TProperty)valueObj;
            }
            return default!;
        }

        /// <summary>
        /// 设置扩展属性项
        /// </summary>
        /// <typeparam name="TProperty">value类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public virtual void SetProperty<TProperty>(string key, TProperty value)
        {
            Properties[key] = value!;
        }
    }
}
