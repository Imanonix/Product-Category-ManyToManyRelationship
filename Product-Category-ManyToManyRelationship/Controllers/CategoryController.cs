using Application.DTOs;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Product_Category_ManyToManyRelationship.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryService;
        public CategoryController(ICategoryServices categoryService)
        {
            _categoryService = categoryService;
        }

        [Route("/GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAllCategory() {

            var rootCategoryList = await _categoryService.GetAllRootCategoryAsync();
            var categoryList = await _categoryService.GetRecursiveNestedCategoriesAsync(rootCategoryList);
            if (categoryList == null)
            {
                return NotFound();
            }

            return Ok(categoryList); //
        }

        [Route("/CreateCategory")]
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                return BadRequest();
            }

            var createdCategory = await _categoryService.AddCategoryAsync(categoryDTO);
            await _categoryService.SaveAsync();
            return CreatedAtAction(nameof(GetCategoryById), new { id = createdCategory.CategoryId }, createdCategory);
        }
        [Route("/GetCategory/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [Route("/Delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete (Guid id)
        {
            var isDeleted = await _categoryService.DeleteCategoryAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
