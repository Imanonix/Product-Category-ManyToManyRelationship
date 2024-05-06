using Domain.Interfaces.Repository;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Implementation
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly ProductCategoryDbContext _context;

        public CategoryRepository(ProductCategoryDbContext context)
        {
            _context = context;
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            return category;
        }

        public async Task<IEnumerable<Category>> GetAllRootCategory()
        {
            var rootCategories = await _context.Categories.Include(c => c.SubCategories).Where(c => c.ParentId == null).ToListAsync();
            return rootCategories?? throw new Exception("list is empty");
        }
        public async Task<IEnumerable<Category>> GetRecursiveNestedCategoriesAsync(Category category)
        {
            var nestedCategories = await _context.Categories.Include(c => c.SubCategories).Where(c => c.ParentId == category.CategoryId).ToListAsync();
            return nestedCategories;
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

            return category ?? throw new Exception("not found");
        }



        public async Task<bool> SaveAsync()
        {
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
