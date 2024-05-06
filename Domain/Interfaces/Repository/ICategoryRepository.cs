using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repository
{
    public interface ICategoryRepository
    {
        Task<Category> AddCategoryAsync(Category category);
        Task<Category> GetByIdAsync(Guid id);
        Task<IEnumerable<Category>> GetAllRootCategory();
        Task<IEnumerable<Category>> GetRecursiveNestedCategoriesAsync(Category category);
        Task<bool> SaveAsync();
    }
}
