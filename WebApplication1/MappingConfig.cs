using AutoMapper;
using LibraryClass.Models;
using LibraryClass.Models.DTO;

namespace WebApplication1
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Item, ItemDTO>().ReverseMap();
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<PlaceOrder, PlaceOrderDTO>().ReverseMap();
            CreateMap<Register, RegisterDTO>().ReverseMap();
        }
    }
}
