using AutoMapper;
using CashRegister.API.Dto;
using CashRegister.Domain.Models;

namespace CashRegister.API.Helpers
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>();
			CreateMap<Product, ProductDto>().ReverseMap();
			CreateMap<Bill, BillDto>();
			CreateMap<Bill, BillDto>().ReverseMap();
		}
    }
}
