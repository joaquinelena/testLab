using AutoMapper;
using JoaquinProducts.DTOs;
using JoaquinProducts.Models;

namespace JoaquinProducts.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<CreateProductDTO, Product>();
            CreateMap<UpdateProductDTO, Product>();
        }
    }
}