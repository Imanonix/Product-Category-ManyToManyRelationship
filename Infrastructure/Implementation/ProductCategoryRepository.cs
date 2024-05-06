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
    }
}
