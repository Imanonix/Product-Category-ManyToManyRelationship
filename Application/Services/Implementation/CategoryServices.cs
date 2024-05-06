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

        public CategoryServices(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
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
                if (item.SubCategories != null)
                {
                    await _categoryRepository.GetRecursiveNestedCategoriesAsync(item);
                    
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


        public async Task<bool> SaveAsync()
        {
            await _categoryRepository.SaveAsync();
            return true;
        }
    }
}
