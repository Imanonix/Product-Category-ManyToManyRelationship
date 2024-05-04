using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid? ParentId { get; set; } 
        public string? ParentName {  get; set; }
        public Category? Parent { get; set; } 
        public ICollection<Category>? SubCategories { get; set;}

        public bool IsActive { get; set; }=true;

        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
