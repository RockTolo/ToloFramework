using Volo.Abp.Domain.Entities.Auditing;

namespace ToloFramework.Entities.Products
{
    public class Product : FullAuditedAggregateRoot<Guid>
    {
        public required string Name {  get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public DateTime Expires { get; set; }

        protected Product() 
        { 
        
        }

        public Product(Guid id) : base(id) 
        { 
        
        }
    }
}
