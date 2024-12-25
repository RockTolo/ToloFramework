using Volo.Abp.Application.Dtos;

namespace ToloFramework.Services.Dtos.Products
{
    public class ProductDto : AuditedEntityDto<Guid>
    {
        public required string Name { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public DateTime Expires { get; set; }
    }
}
