using AutoMapper;
using CashRegister.API.Dto;
using CashRegister.Application.Dto;
using CashRegister.Application.Interfaces;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;

namespace CashRegister.Application.Services
{
	public class BillService : IBillService
	{
		private readonly IBillRepository _billRepository;
		private readonly IMapper _mapper;
		
		private readonly IPriceCalculatorService _priceCalculator;
		public BillService(IBillRepository billRepository,IMapper mapper, IPriceCalculatorService priceCalculatorService)
		{
			_billRepository = billRepository;
			_mapper = mapper;
			_priceCalculator = priceCalculatorService;
		}
		public async Task<List<DisplayBillDto>> GetAllBillsAsync()
		{
			var bills = await _billRepository.GetAllBillsAsync();
			var billsMapped = _mapper.Map<List<DisplayBillDto>>(bills);
			return billsMapped;
		}
		public async Task<DisplayBillDto> GetBillByBillNumberAsync(string billNumber)
		{
			var bill = await _billRepository.GetBillByBillNumberAsync(billNumber);
			var billMapped = _mapper.Map<DisplayBillDto>(bill);
			return billMapped;
		}
		public async Task<Bill> GetBillByBillNumberAsNoTracking(string billNumber)
		{
			var bill = await _billRepository.GetBillByBillNumberAsNoTracking(billNumber);
			return bill;
		}
		public bool AddBill(AddBillDto billDto)
		{
			var billMapped = _mapper.Map<Bill>(billDto);

			return _billRepository.AddBill(billMapped);
		}
		public bool DeleteBill(Bill bill)
		{
			return _billRepository.DeleteBill(bill);
		}
		public bool UpdateBill(string billNumber, AddBillDto billDto)
		{
			var billMapped = _mapper.Map<Bill>(billDto);
			billMapped.BillNumber = billNumber;

			return _billRepository.UpdateBill(billMapped);
		}

		public async Task<DisplayBillDto> DisplayBill(DisplayBillDto billDto, string currency)
		{
			billDto.TotalPrice = _priceCalculator.CurrencyExchange(billDto.TotalPrice, currency);
			return billDto;
		}
		public bool BillExists(string billNumber)
		{
			return _billRepository.BillExists(billNumber);
		}
	}
}
