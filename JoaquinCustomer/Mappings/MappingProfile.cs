using AutoMapper;
using JoaquinCustomer.DTOs;
using JoaquinCustomer.Models;

namespace JoaquinCustomer.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CreateCustomerDTO, Customer>();
            CreateMap<UpdateCustomerDTO, Customer>();
        }
    }
}