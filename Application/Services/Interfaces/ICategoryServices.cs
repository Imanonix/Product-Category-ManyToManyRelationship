using Application.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ICategoryServices
    {
        Task<CategoryDTO> AddCategoryAsync(CategoryDTO categoryDTO);
        Task<CategoryDTO> GetByIdAsync(Guid id);
        Task<IEnumerable<Category>> GetAllRootCategoryAsync();
        Task<IEnumerable<Category>> GetRecursiveNestedCategoriesAsync(IEnumerable<Category> categoriesList);
        Task<bool> SaveAsync();
    }
}
