using Application.DTOs;
using AutoMapper;
using Domain.Models;


namespace Application.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTOAdd>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
