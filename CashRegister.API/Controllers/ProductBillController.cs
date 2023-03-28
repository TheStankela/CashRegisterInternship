using AutoMapper;
using CashRegister.API.Dto;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
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
		private readonly IBillRepository _billRepository;
		private readonly IProductRepository _productRepository;
		public ProductBillController(IProductBillRepository productBillRepository, IMapper mapper, IProductRepository productRepository, IBillRepository billRepository)
        {
			_productBillRepository = productBillRepository;
            _mapper = mapper;
            _billRepository = billRepository;
            _productRepository = productRepository;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllProductBills()
		{
			var result = await _productBillRepository.GetAllProductBills();

			return Ok(result);
		}

		[HttpPost]
        public async Task<IActionResult> AddProductToBill(int productId, string billNumber, ProductBillDto productBillDto)
        {
            if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (_productBillRepository.ProductBillExists(productId, billNumber))
				return NotFound("Entered products already added to bill.");

            if (_productRepository.ProductExists(productId))
                return NotFound("Entered product does not exist.");

            if (_billRepository.BillExists(billNumber))
                return NotFound("Entered bill does not exist.");

            var productBillMapped = _mapper.Map<ProductBill>(productBillDto);

			if (!await _productBillRepository.AddProductToBill(productId, billNumber, productBillMapped))
                return BadRequest();

			return Ok("Product added to bill successfully!");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProductToBill(int productId, string billNumber)
        {
            if (!_productBillRepository.ProductBillExists(productId, billNumber))
                return NotFound("ProductBill does not exist.");
            
            if(!await _productBillRepository.DeleteProductFromBill(productId, billNumber))
                return BadRequest();

            return Ok("Successfully deleted");
        }

    }
}
