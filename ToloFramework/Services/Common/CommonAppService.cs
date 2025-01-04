namespace ToloFramework.Services.Common
{
    public class CommonAppService : ToloFrameworkAppService, ICommonAppService
    {
        public async Task HttpClientRequestAsync()
        {
            var httpClientFactory = LazyServiceProvider.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient("baidu");
            httpClient.DefaultRequestVersion = new Version(2, 0);
            var response = await httpClient.GetAsync("https://pss.bdstatic.com/r/www/cache/static/aladdin-san/app/right_recommends_merge/result_1c1a9d0.js");
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine("baidu 请求完成");

            var qqClient = httpClientFactory.CreateClient("bing");
            var qqResponse = await qqClient.GetAsync("https://cn.bing.com");
            var qqResult = await qqResponse.Content.ReadAsStringAsync();
            Console.WriteLine("bing 请求完成");
        }
    }
}
