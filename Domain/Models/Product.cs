using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }= string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public string ProductCode { get; set; }= string.Empty;
        public decimal? Discount { get; set; }
        public DateTime? DiscountTime { get; set; }

        public decimal? Price { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
        public bool IsAvailable { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<ProductImage> ProductImages { get;}
    }
}
