
namespace Application.DTOs
{
    public class CategoryDTO
    {
        public Guid? CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid? ParentId { get; set; }
        public string? ParentName { get; set; }
    }
}
