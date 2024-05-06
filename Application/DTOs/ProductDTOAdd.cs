using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProductDTOAdd
    {
        public Guid? ProductId { get; set; }
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ProductCode { get; set; } = string.Empty;
        public decimal? Discount { get; set; }
        public DateTime? DiscountTime { get; set; }
        public decimal? Price { get; set; }

        public List<Guid> CategoriesList { get; set; }= [];
    }
}
