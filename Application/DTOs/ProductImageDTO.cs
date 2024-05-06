using System.ComponentModel.DataAnnotations;


namespace Application.DTOs
{
    public class ProductImageDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }= string.Empty;

        public bool IsFeatured { get; set; }

        public Guid ProductId { get; set; }
    }
}
