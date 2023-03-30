using AutoMapper;
using CashRegister.API.Dto;
using CashRegister.Application.Dto;
using CashRegister.Domain.Models;

namespace CashRegister.API.Helpers
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>();
			CreateMap<Product, ProductDto>().ReverseMap();
			CreateMap<Bill, AddBillDto>();
			CreateMap<Bill, AddBillDto>().ReverseMap();
			CreateMap<ProductBill, ProductBillDto>();
			CreateMap<ProductBill, ProductBillDto>().ReverseMap();
			CreateMap<Bill, DisplayBillDto>();
			CreateMap<Bill, DisplayBillDto>().ReverseMap();
		}
    }
}
