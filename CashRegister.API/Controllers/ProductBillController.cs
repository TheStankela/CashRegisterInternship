using AutoMapper;
using CashRegister.API.Dto;
using CashRegister.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CashRegister.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductBillController : ControllerBase
    {
		private readonly IProductBillRepository _productBillRepository;
        private readonly IMapper _mapper;
		public ProductBillController(IProductBillRepository productBillRepository, IMapper mapper)
        {
			_productBillRepository = productBillRepository;
            _mapper = mapper;
		}
        [HttpPost]
        public async Task<IActionResult> AddProductToBill(int id, string billNumber, ProductBillApiDto productBillDto)
        {
            var productBillDtoMapped = _mapper.Map<ProductBillRepoDto>(productBillDto);

            if (!await _productBillRepository.AddProductBill(id, billNumber, productBillDtoMapped))
                return BadRequest();

			return Ok("Product added to bill successfully!");
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProductBills()
        {
            var result = await _productBillRepository.GetAllProductBills();

            return Ok(result);
        }

    }
}
