using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using ToloFramework.Services.Dtos.Products;

namespace ToloFramework.Services.Products
{
    public interface IProductAppService : ICrudAppService<
        ProductDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateProductDto>
    {
        Task CreateManyAsync(int dataCount);

        Task DeleteManyAsync(int dataCount);

        Task UpdateManyAsync(int dataCount);
    }
}
