using System.ComponentModel.DataAnnotations;

namespace ToloFramework.Services.Dtos.Products
{
    public class CreateUpdateProductDto
    {
        [Required]
        [StringLength(128)]
        public required string Name { get; set; }

        [Required]
        [StringLength(256)]
        public required string Title { get; set; }

        [Required]
        [StringLength(512)]
        public required string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        [Required]
        public DateTime Expires { get; set; }
    }
}
