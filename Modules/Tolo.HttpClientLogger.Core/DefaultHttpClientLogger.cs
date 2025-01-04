namespace Tolo.HttpClientLogger
{
    public class DefaultHttpClientLogger : ICustomHttpClientLogger
    {
        protected HttpClientLogInfo? LogInfo { get; set; }

        public DefaultHttpClientLogger()
        {
            
        }

        public virtual async ValueTask LogRequestStartAsync(
            LogContext context,
            CancellationToken cancellationToken = default)
        {
            await RequestStartBuildDataAsync(context);
        }

        public virtual async ValueTask LogRequestStopAsync(
            LogContext context,
            CancellationToken cancellationToken = default)
        {
            await RequestEndBuildDataAsync(context);
        }

        public virtual async ValueTask LogRequestFailedAsync(
           LogContext context,
           CancellationToken cancellationToken = default)
        {
            await RequestEndBuildDataAsync(context);
        }

        protected virtual async ValueTask RequestStartBuildDataAsync(LogContext context)
        {
            var requestUrl = context.Request.RequestUri.GetLeftPart(UriPartial.Authority) + context.Request.RequestUri.AbsolutePath;
            LogInfo = new HttpClientLogInfo(
              context.ClientName,
              requestUrl,
              context.Request.Method.ToString());
            
            LogInfo.QueryString = context.Request.RequestUri.Query;
            LogInfo.RequestHeaders = new HttpHeadersValue(
                HttpHeadersValue.Kind.Request,
                context.Request.Headers,
                context.Request.Content?.Headers,
                context.HttpClientFactoryOptions.ShouldRedactHeaderValue).ToString();
            LogInfo.RequstPayload = context.Request.Content == null ? null : 
                (await context.Request.Content.ReadAsStringAsync());
            LogInfo.RequstPayload = SubBodyLimit(LogInfo.RequstPayload, context.HttpClientLogOptions.ResponseBodyLogLimit);
        }

        protected virtual async ValueTask RequestEndBuildDataAsync(LogContext context)
        {
            LogInfo!.RequestDuration = Convert.ToInt32(context.Elapsed.TotalMilliseconds);            

            if (context.Response is not null)
            {
                LogInfo!.ResponseHeaders = new HttpHeadersValue(
                HttpHeadersValue.Kind.Response,
                context.Response.Headers,
                context.Response?.Content?.Headers,
                context.HttpClientFactoryOptions.ShouldRedactHeaderValue).ToString();
                LogInfo.StatusCode = (int)context.Response!.StatusCode;
                LogInfo.Response = context.Response?.Content == null ? null :
                    (await context.Response.Content.ReadAsStringAsync());
                LogInfo.Response = SubBodyLimit(LogInfo.Response, context.HttpClientLogOptions.ResponseBodyLogLimit);
            }

            if (context.Exception is not null)
            {
                LogInfo.ExceptionMessage = context.HttpClientLogOptions.IsExceptionToStackTrace ? 
                    context.Exception.StackTrace : context.Exception.Message;
            }
        }

        protected virtual void shouldRedactHeaderValue()
        { 
            
        }

        private string? SubBodyLimit(string? body, int limit)
        {
            if (string.IsNullOrWhiteSpace(body))
            {
                return body;
            }

            if (body!.Length > limit)
            {
                return body.Substring(0, limit);
            }

            return body;
        }
    }
}
