using ToloFramework.Entities.Products;
using ToloFramework.Services.Dtos.Products;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace ToloFramework.Services.Products
{
    public class ProductAppService : CrudAppService<
        Product, ProductDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateProductDto>,
        IProductAppService
    {
        public ProductAppService(IRepository<Product, Guid> repository) : base(repository)
        {

        }

        public async Task CreateManyAsync(int dataCount)
        {
            var list = new List<Product>();
            for (int i = 1; i <= dataCount; i++)
            {
                list.Add(new Product(GuidGenerator.Create())
                {
                    Name = $"产品{i}",
                    Title = $"标题{i}",
                    Description = $"说明{i}",
                    Price = 100 * i,
                    Expires = Clock.Now.AddDays(i)
                });
            }
            await Repository.InsertManyAsync(list);
            Console.WriteLine("批量添加数据成功！");
        }

        public async Task UpdateManyAsync(int dataCount)
        {
            ReadOnlyRepository.DisableTracking();
            var list = await ReadOnlyRepository.GetPagedListAsync(0, dataCount, nameof(IHasCreationTime.CreationTime) + " desc");

            foreach (var item in list)
            {
                item.Name += "-改";
                item.Title += "-改";
                item.Description += "-改";
            }
            await Repository.UpdateManyAsync(list);
            Console.WriteLine("批量修改数据成功！");
        }

        public async Task DeleteManyAsync(int dataCount)
        {
            var list = await ReadOnlyRepository.GetPagedListAsync(0, dataCount, nameof(IHasCreationTime.CreationTime) + " desc");
            await Repository.DeleteManyAsync(list);
            Console.WriteLine("批量删除数据成功！");
        }
    }
}
