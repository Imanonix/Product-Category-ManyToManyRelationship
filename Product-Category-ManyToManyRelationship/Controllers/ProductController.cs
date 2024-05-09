using Application.DTOs;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Product_Category_ManyToManyRelationship.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;
        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productServices.GetProductByIdAsync(id);
            return Ok(product);
        }

        [Route("/CreateProduct")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductDTOAdd productDTOAdd)
        {
            if (productDTOAdd == null)
            {
                return BadRequest("information not completed");
            }
            var createdProduct = await _productServices.AddProductAsync(productDTOAdd);
            //return CreatedAtAction("GetProductById", new { id = createdProduct.ProductId, createdProduct });
            return Ok();
        }

    }
}
