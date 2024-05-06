
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Category
    {
        [Required]
        public Guid CategoryId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public Guid? ParentId { get; set; } 
        public string? ParentName {  get; set; }
        [JsonIgnore]
        public Category Parent { get; set; } 
        public ICollection<Category>? SubCategories { get; set;}

        public bool IsActive { get; set; }=true;

        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
