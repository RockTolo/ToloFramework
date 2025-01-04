using System.Text;

namespace Tolo.HttpClientLogger
{
    public class HttpClientLogInfo
    {
        /// <summary>
        /// HttpClient名称
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 请求的地址
        /// </summary>
        public string RequestUrl { get; set; }

        /// <summary>
        /// 请求方法
        /// </summary>
        public string RequestMethod { get; set; }

        /// <summary>
        /// 请求头
        /// </summary>
        public string? RequestHeaders { get; set; }

        /// <summary>
        /// 查询字符串参数
        /// </summary>
        public string? QueryString { get; set; }

        /// <summary>
        /// 请求载荷
        /// </summary>
        public string? RequstPayload { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime RequestTime { get; set; }

        /// <summary>
        /// 响应头
        /// </summary>
        public string? ResponseHeaders { get; set; }

        /// <summary>
        /// 响应的内容
        /// </summary>
        public string? Response { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 请求总持续时间(毫秒)
        /// </summary>
        public int RequestDuration { get; set; }

        /// <summary>
        /// 请求时发生的异常
        /// </summary>
        public string? ExceptionMessage { get; set; }

        public HttpClientLogInfo(
            string clientName, 
            string requestUrl,
            string requestMethod)
        {
            ClientName = clientName;
            RequestUrl = requestUrl;
            RequestMethod = requestMethod;
            RequestTime = DateTime.Now;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("HttpClient LogInfo: ");
            sb.Append($"{nameof(ClientName)}: ").AppendLine(ClientName);
            sb.Append($"{nameof(RequestUrl)}: ").AppendLine(RequestUrl);
            sb.Append($"{nameof(RequestMethod)}: ").AppendLine(RequestMethod);
            sb.Append($"{nameof(RequestHeaders)}: ").AppendLine(RequestHeaders);
            sb.Append($"{nameof(QueryString)}: ").AppendLine(QueryString);
            sb.Append($"{nameof(RequstPayload)}: ").AppendLine(RequstPayload);
            sb.Append($"{nameof(ResponseHeaders)}: ").AppendLine(ResponseHeaders);
            sb.Append($"{nameof(Response)}: ").AppendLine(Response);
            sb.Append($"{nameof(RequestTime)}: ").AppendLine(RequestTime.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.Append($"{nameof(StatusCode)}: ").AppendLine(StatusCode.ToString());
            sb.Append($"{nameof(RequestDuration)}: ").AppendLine(RequestDuration.ToString());
            sb.Append("Result: ").AppendLine(ExceptionMessage ?? "succeed");

            return sb.ToString();
        }
    }
}
