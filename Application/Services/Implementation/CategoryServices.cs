using Application.DTOs;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Interfaces.Repository;
using Domain.Models;


namespace Application.Services.Implementation
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public CategoryServices(ICategoryRepository categoryRepository, IMapper mapper, IProductCategoryRepository productCategoryRepository)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<CategoryDTO> AddCategoryAsync(CategoryDTO categoryDTO)
        {
            Category category = _mapper.Map<Category>(categoryDTO);

            await _categoryRepository.AddCategoryAsync(category);
            await _categoryRepository.SaveAsync();
            categoryDTO = _mapper.Map<CategoryDTO>(category);

            return categoryDTO;
        }

        public async Task<IEnumerable<Category>> GetAllRootCategoryAsync()
        {
            var rootCategories = await _categoryRepository.GetAllRootCategory();

            return rootCategories;
        }

        public async Task<IEnumerable<Category>> GetRecursiveNestedCategoriesAsync(IEnumerable<Category> categoriesList)
        {
            foreach (var item in categoriesList)
            {
                var nestedCategories = await _categoryRepository.GetNestedCategoriesAsync(item);
                if (nestedCategories.Any())
                {
                    await GetRecursiveNestedCategoriesAsync(nestedCategories);
                }
            }

            return categoriesList;
        }


        public async Task<CategoryDTO> GetByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return null;
            }
            var categoryDTO = _mapper.Map<CategoryDTO>(category);
            return categoryDTO;
        }

        public async Task<CategoryDTO> UpdateCategoryAsync(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.UpdateCategoryAsync(category);
            await _categoryRepository.SaveAsync();
            categoryDTO = _mapper.Map<CategoryDTO>(category);
            return categoryDTO;
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return false;
            }
            await DeleteCategoryAndSubCategoryRecursiveAsync(category);

            await _categoryRepository.SaveAsync();
            return true;
        }

        public async Task DeleteCategoryAndSubCategoryRecursiveAsync(Category category)
        {
            var listSubCategories = await _categoryRepository.GetNestedCategoriesAsync(category);
            if (listSubCategories.Count() == 0)
            {
                await _categoryRepository.DeleteCategoryAsync(category);
                await _productCategoryRepository.DeleteByCategoryIdAsync(category);
            }
            else
            {
                foreach (var sub in listSubCategories)
                {
                    await DeleteCategoryAndSubCategoryRecursiveAsync(sub);
                }
                await _categoryRepository.DeleteCategoryAsync(category); //After deleting its childs, we delete the category itself
                await _productCategoryRepository.DeleteByCategoryIdAsync(category); //deleting category from ProductCategory table
            }
        }

        public async Task<bool> SaveAsync()
        {
            await _categoryRepository.SaveAsync();
            return true;
        }
    }
}
