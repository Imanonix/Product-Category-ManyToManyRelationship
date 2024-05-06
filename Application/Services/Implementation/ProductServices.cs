using Application.DTOs;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Interfaces.Repository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Implementation
{
    public class ProductServices : IProductServices
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductServices(IMapper mapper, IProductRepository productRepository, IProductCategoryRepository productCategoryRepository, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<ProductDTO> AddProductAsync(ProductDTOAdd productDTOAdd)
        {
            var product = _mapper.Map<Product>(productDTOAdd);
            product = await _productRepository.AddProductAsync(product);
            

            foreach (var categoryId in productDTOAdd.CategoriesList)
            {
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                await _productCategoryRepository.AddAsync(new ProductCategory
                {
                    Product = product,
                    Category = category
                });
            }

            await _productRepository.SaveAsync();

            var productDTO = _mapper.Map<ProductDTO>(product);
            return productDTO;
        }

        public async Task<ProductDTO> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            var productDTO = new ProductDTO
            {
                ProductId = product.ProductId,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                Discount = product.Discount,
                DiscountTime = product.DiscountTime,

                CategoriesDTO = product.ProductCategories.Select(pc => new CategoryDTO
                {
                    CategoryId = pc.CategoryId,
                    Name = pc.Category.Name,
                    ParentName = pc.Category.ParentName,
                    ParentId = pc.Category.ParentId,
                }).ToList()
            };

            //var productDTO = _mapper.Map<Product, ProductDTO>( product);
            return productDTO;
        }
    }
}
