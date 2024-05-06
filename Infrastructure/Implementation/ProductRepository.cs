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
    public class ProductRepository : IProductRepository
    {
        private readonly ProductCategoryDbContext _context;

        public ProductRepository(ProductCategoryDbContext context)
        {
            _context = context;
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);

            return product;
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            var product = await _context.Products.Include(p=> p.ProductCategories).ThenInclude(p => p.Category).SingleOrDefaultAsync(p => p.ProductId == id );
            return product ?? throw new Exception("not found");
        }

        public async Task<bool> SaveAsync()
        {
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
