using System.ComponentModel.DataAnnotations;


namespace Domain.Models
{
    public class ProductImage
    {
        [Key]
        public Guid ImageId { get; set; }
        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        public bool IsFeatured { get; set; }

        public Guid ProductId { get; set; }

        public required Product Product { get; set; }
    }
}

