using Application.Config;
using Application.DTOs;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Interfaces.Repository;
using Domain.Models;
using Microsoft.Extensions.Options;
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
        private readonly Address _address;
        private readonly IProductImageRepository _productImageRepository;
        

        public ProductServices(IMapper mapper, IProductRepository productRepository, IProductCategoryRepository productCategoryRepository, ICategoryRepository categoryRepository, IProductImageRepository productImageRepository, IOptions<Address> address)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _categoryRepository = categoryRepository;
            _address = address.Value;
            _productImageRepository = productImageRepository;
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

            #region Add images
            string baseUrl = _address.Scheme + "://" + _address.Host;
            foreach (var image in productDTOAdd.ImagesList)
            {
                string fileName = Guid.NewGuid().ToString() + image.FileName;
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload/product/", product.ProductId.ToString());
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var imagePath = Path.Combine(folderPath, fileName);
                using (FileStream stream = new FileStream(imagePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
                ProductImage productImage = new ProductImage();

                productImage.ImageUrl = baseUrl+"/upload/product/"+ product.ProductId.ToString()+"/"+fileName;
                productImage.ProductId = product.ProductId; 
                await _productImageRepository.AddImageAsync(productImage);
                await _productRepository.SaveAsync();
            }

            #endregion

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
