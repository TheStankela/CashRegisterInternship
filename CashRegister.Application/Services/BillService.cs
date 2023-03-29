using AutoMapper;
using CashRegister.API.Dto;
using CashRegister.Application.Interfaces;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;

namespace CashRegister.Application.Services
{
	public class BillService : IBillService
	{
		private readonly IBillRepository _billRepository;
		private readonly IPriceCalculatorService _priceCalculatorService;
		private readonly IMapper _mapper;
		public BillService(IBillRepository billRepository,IPriceCalculatorService priceCalculatorService ,IMapper mapper)
		{
			_billRepository = billRepository;
			_priceCalculatorService = priceCalculatorService;
			_mapper = mapper;
		}
		public async Task<List<Bill>> GetAllBillsAsync()
		{
			return await _billRepository.GetAllBillsAsync();
		}
		public async Task<Bill> GetBillByBillNumberAsync(string billNumber)
		{
			return await _billRepository.GetBillByBillNumberAsync(billNumber);
		}
		public async Task<bool> AddBill(BillDto billDto)
		{
			var billMapped = _mapper.Map<Bill>(billDto);

			return _billRepository.AddBill(billMapped);
		}
		public async Task<bool> DeleteBill(string billNumber)
		{
			var billToDelete = await _billRepository.GetBillByBillNumberAsync(billNumber);

			return _billRepository.DeleteBill(billToDelete);
		}
		public bool UpdateBill(string billNumber, BillDto billDto)
		{
			if (! _billRepository.BillExists(billNumber))
				return false;

			var billMapped = _mapper.Map<Bill>(billDto);
			billMapped.BillNumber = billNumber;

			return _billRepository.UpdateBill(billMapped);
		}
	}
}
