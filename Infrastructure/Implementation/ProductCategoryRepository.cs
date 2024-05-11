using Domain.Interfaces.Repository;
using Domain.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ProductCategoryDbContext _context;

        public ProductCategoryRepository(ProductCategoryDbContext context)
        {
            _context = context;
        }
        public async Task<ProductCategory> AddAsync(ProductCategory productCategory)
        {
            await _context.ProductCategories.AddAsync(productCategory);
            return productCategory;
        }
        public async Task<ProductCategory> DeleteAsync(ProductCategory productCategory)
        {
            if (!_context.ProductCategories.Local.Any(c => c.CategoryId == productCategory.CategoryId && c.ProductId == productCategory.ProductId))
            {
                // Attach the entity if it's detached
                _context.Attach(productCategory);
            }

            _context.Entry(productCategory).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return productCategory;
        }

        public async Task<bool> DeleteByCategoryIdAsync(Category category)
        {
            var listCategory = await _context.ProductCategories.Where(pc => pc.CategoryId == category.CategoryId).ToListAsync();
            _context.ProductCategories.RemoveRange(listCategory);
            _context.SaveChanges();
            return true;
        }
    }
}
