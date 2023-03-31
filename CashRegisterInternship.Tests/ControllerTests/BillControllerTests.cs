using AutoMapper;
using CashRegister.API.Controllers;
using CashRegister.API.Dto;
using CashRegister.API.Mediator.Querries.BillQuerries;
using CashRegister.API.Mediator.Querries.ProductQuerries;
using CashRegister.Application.Dto;
using CashRegister.Domain.Models;
using CashRegister.Infrastructure.Repositories;
using FluentAssertions;
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
		public async Task BillController_GetAllBills_ShouldReturnList()
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
		public async Task BillController_GetAllBills_ShouldReturnOKBill()
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
	}
}
