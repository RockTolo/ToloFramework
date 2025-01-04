using Volo.Abp.Application.Services;

namespace ToloFramework.Services.Common
{
    public interface ICommonAppService : IApplicationService
    {
        Task HttpClientRequestAsync();
    }
}
