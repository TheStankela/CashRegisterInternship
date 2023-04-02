using CashRegister.API.Controllers;
using CashRegister.API.Dto;
using CashRegister.API.Mediator.Commands.ProductBillCommands;
using CashRegister.API.Mediator.Querries.ProductBillQuerries;
using CashRegister.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegisterInternship.Tests.ControllerTests
{
	public class ProductBillControllerTests
	{
		private readonly Mock<IMediator> _mediator = new Mock<IMediator>();
		private ProductBillController _sut;
		public ProductBillControllerTests()
		{
			_sut = new ProductBillController(_mediator.Object);
		}
		[Fact]
		public async Task ProductBill_GetAllProductBills_ReturnsOKList()
		{
			//Arrange
			var productBill = new ProductBill { BillNumber = "260-0056010016113-79", ProductId = 3 };
			var productBills = new List<ProductBill> { productBill };

			_mediator.Setup(c => c.Send(It.IsAny<GetAllProductBillsQuerry>(), It.IsAny<CancellationToken>())).ReturnsAsync(productBills);

			//Act
			var result = await _sut.GetAllProductBills();
			var okResult = result as OkObjectResult;

			//Assert
			Assert.NotNull(okResult);
			Assert.IsType<OkObjectResult>(okResult);
			Assert.IsType<List<ProductBill>>(okResult.Value);
			Assert.Equal(okResult.Value, productBills);
		}
		[Fact]
		public async Task ProductBill_AddProductToBill_Returns200True()
		{
			//Arrange
			string billNumber = "260-0056010016113-79";
			int productId = 3;
			var productBill = new ProductBillDto { ProductQuantity = 3 };

			_mediator.Setup(c => c.Send(It.IsAny<AddProductToBillCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

			//Act
			var result = await _sut.AddProductToBill(productId, billNumber, productBill);
			var objResult = result as ObjectResult;

			//Assert
			Assert.NotNull(objResult);
			Assert.IsType<ObjectResult>(objResult);
			Assert.IsType<bool>(objResult.Value);
			Assert.Equal(true, objResult.Value);
			Assert.Equal(StatusCodes.Status200OK, objResult.StatusCode);
		}
		[Fact]
		public async Task ProductBill_DeleteProductFromBill_Returns200True()
		{
			//Arrange
			string billNumber = "260-0056010016113-79";
			int productId = 3;

			_mediator.Setup(c => c.Send(It.IsAny<DeleteProductFromBillCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

			//Act
			var result = await _sut.DeleteProductFromBill(productId, billNumber);
			var objResult = result as ObjectResult;

			//Assert
			Assert.NotNull(objResult);
			Assert.IsType<ObjectResult>(objResult);
			Assert.IsType<bool>(objResult.Value);
			Assert.Equal(true, objResult.Value);
			Assert.Equal(StatusCodes.Status200OK, objResult.StatusCode);
		}
	}
}
