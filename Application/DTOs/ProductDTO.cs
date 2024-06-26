﻿using System.ComponentModel.DataAnnotations;


namespace Application.DTOs
{
    public class ProductDTO
    {
        public Guid? ProductId { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }= string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ProductCode { get; set; } = string.Empty;
        public decimal? Discount { get; set; }
        public DateTime? DiscountTime { get; set; }
        public decimal? Price { get; set; }

        public ICollection<CategoryDTO>? CategoriesDTO { get; set; }
        
    }
}
