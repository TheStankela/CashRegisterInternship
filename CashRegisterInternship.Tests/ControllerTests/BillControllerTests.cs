using CashRegister.API.Controllers;
using CashRegister.API.Dto;
using CashRegister.API.Mediator.Commands.BillCommands;
using CashRegister.API.Mediator.Querries.BillQuerries;
using CashRegister.Application.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CashRegisterInternship.Tests.ControllerTests
{
	public class BillControllerTests
	{
		private readonly Mock<IMediator> _mediatorMock = new Mock<IMediator>();
		private readonly BillController _sut;


		public BillControllerTests()
		{
			_sut = new BillController(_mediatorMock.Object);
		}
		[Fact]
		public async Task BillController_GetAllBills_ShouldReturnOKList()
		{
			List<DisplayBillDto> expected = new List<DisplayBillDto> { new DisplayBillDto {BillNumber = "260-0056010016113-79", 
				PaymentMethod = "Cash", TotalPrice = 100, CreditCardNumber = "371449635398431" } };

			var querry = new Mock<GetAllBillsQuerry>();
			_mediatorMock
					.Setup(s => s.Send(It.IsAny<GetAllBillsQuerry>(), It.IsAny<CancellationToken>()))
					.ReturnsAsync(expected);

			//Act
			var result = await _sut.GetAllBills();
			var okResult = result as ObjectResult;

			//Assert
			Assert.NotNull(okResult);
			Assert.True(okResult is OkObjectResult);
			Assert.IsType<List<DisplayBillDto>>(okResult.Value);
			Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
		}
		[Fact]
		public async Task BillController_GetBillById_ShouldReturnOKBill()
		{
			DisplayBillDto expected = new DisplayBillDto
			{
				BillNumber = "260-0056010016113-79",
				PaymentMethod = "Cash",
				TotalPrice = 100,
				CreditCardNumber = "371449635398431"
			};

			var querry = new Mock<GetBillByNumberQuerry>();
			_mediatorMock
					.Setup(s => s.Send(It.IsAny<GetBillByNumberQuerry>(), It.IsAny<CancellationToken>()))
					.ReturnsAsync(expected);

			//Act
			var result = await _sut.GetBillByBillNumberAsync("260-0056010016113-79");
			var okResult = result as ObjectResult;

			//Assert
			Assert.NotNull(okResult);
			Assert.True(okResult is OkObjectResult);
			Assert.IsType<DisplayBillDto>(okResult.Value);
			Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
		}
		[Fact]
		public async Task BillController_GetBillWithUpdatedCurrency_ShouldReturnBillDto()
		{
			string billNumber = "260-0056010016113-79";
			string currency = "eur";
			var expected = new DisplayBillDto { BillNumber = billNumber, TotalPrice = 100 };

			var querry = new Mock<GetBillWithUpdatedCurrencyQuerry>();
			_mediatorMock.Setup(s => s.Send(It.IsAny<GetBillWithUpdatedCurrencyQuerry>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

			//Act
			var result = await _sut.GetBillWithUpdatedCurrency(billNumber, currency);
			var okResult = result as ObjectResult;

			//Assert
			Assert.True(okResult is OkObjectResult);
			Assert.IsType<DisplayBillDto> (okResult.Value);
			Assert.Equal(okResult.Value, expected);
			Assert.Equal(StatusCodes.Status200OK, okResult?.StatusCode);
		}
		[Fact]
		public async Task BillController_AddBill_ShouldReturn200True()
		{
			//Arrange
			var billDto = new AddBillDto
			{
				BillNumber = "260-0056010016113-79",
				PaymentMethod = "Cash",
				CreditCardNumber = "371449635398431"
			};

			var querry = new Mock<CreateBillCommand>();
			_mediatorMock.Setup(m => m.Send(It.IsAny<CreateBillCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

			//Act
			var result = await _sut.AddBill(billDto);
			var objResult = result as ObjectResult;

			//Assert
			Assert.IsType<ObjectResult>(objResult);
			Assert.IsType<bool>(objResult.Value);
			Assert.Equal(true, objResult.Value);
			Assert.Equal(StatusCodes.Status200OK, objResult.StatusCode);
		}
		[Fact]
		public async Task BillController_UpdateBill_ShouldReturn200True()
		{
			//Arrange
			string billNumber = "260-0056010016113-79";
			var billDto = new AddBillDto
			{
				BillNumber = billNumber,
				PaymentMethod = "Cash",
				CreditCardNumber = "371449635398431"
			};

			var querry = new Mock<UpdateBillCommand>();
			_mediatorMock.Setup(m => m.Send(It.IsAny<UpdateBillCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

			//Act
			var result = await _sut.UpdateBill(billNumber, billDto);
			var objResult = result as ObjectResult;

			//Assert
			Assert.IsType<ObjectResult>(objResult);
			Assert.IsType<bool>(objResult.Value);
			Assert.Equal(true, objResult.Value);
			Assert.Equal(StatusCodes.Status200OK, objResult.StatusCode);
		}
		[Fact]
		public async Task BillController_DeleteBill_ShouldReturn200True()
		{
			//Arrange
			string billNumber = "260-0056010016113-79";

			var querry = new Mock<DeleteBillCommand>();
			_mediatorMock.Setup(m => m.Send(It.IsAny<DeleteBillCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

			//Act
			var result = await _sut.DeleteBill(billNumber);
			var objResult = result as ObjectResult;

			//Assert
			Assert.IsType<ObjectResult>(objResult);
			Assert.IsType<bool>(objResult.Value);
			Assert.Equal(true, objResult.Value);
			Assert.Equal(StatusCodes.Status200OK, objResult.StatusCode);
		}
		
	}
}
