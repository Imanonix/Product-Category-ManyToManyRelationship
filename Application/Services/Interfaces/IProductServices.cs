using Application.DTOs;


namespace Application.Services.Interfaces
{
    public interface IProductServices
    {
        Task<ProductDTO> AddProductAsync(ProductDTOAdd productDTOAdd);
        Task<ProductDTO> GetProductByIdAsync(Guid id);
    }
}
