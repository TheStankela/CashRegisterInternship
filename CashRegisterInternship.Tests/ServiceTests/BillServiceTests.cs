using AutoMapper;
using CashRegister.API.Dto;
using CashRegister.Application.Dto;
using CashRegister.Application.Interfaces;
using CashRegister.Application.Services;
using CashRegister.Domain.Interfaces;
using CashRegister.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegisterInternship.Tests.ServiceTests
{
	public class BillServiceTests
	{
		public Mock<IBillRepository> _billRepoMock = new Mock<IBillRepository>();
		public Mock<IMapper> _mapperMock = new Mock<IMapper>();
		public Mock<IPriceCalculatorService> _calculatorService = new Mock<IPriceCalculatorService>();
		public BillService _sut;

		public BillServiceTests()
		{
			_sut = new BillService(_billRepoMock.Object, _mapperMock.Object, _calculatorService.Object);
		}
		[Fact]
		public async Task BillService_GetAllBills_ReturnsListOfBillDtos()
		{
			//Arrange
			List<Bill> bills = new List<Bill> { new Bill { BillNumber = "", CreditCardNumber = "", PaymentMethod = "", TotalPrice = 100} };
			var mappedBills = new List<DisplayBillDto> { new DisplayBillDto { BillNumber = bills[0].BillNumber, TotalPrice = bills[0].TotalPrice,
				PaymentMethod = bills[0].PaymentMethod, CreditCardNumber = bills[0].CreditCardNumber } };

			_mapperMock.Setup(m => m.Map<List<DisplayBillDto>>(bills)).Returns(mappedBills);
			_billRepoMock.Setup(b => b.GetAllBillsAsync()).ReturnsAsync(bills);

			//Act
			var result = await _sut.GetAllBillsAsync();

			//Assert
			Assert.NotNull(result);
			Assert.Equal(result, mappedBills);
			Assert.IsType<List<DisplayBillDto>>(result);
		}
		[Fact]
		public async Task BillService_GetBillByNumber_ReturnsDisplayBillDto()
		{
			//Arrange
			var billNumber = "123";
			var bill = new Bill { BillNumber = billNumber, CreditCardNumber = "", PaymentMethod = "" };
			var mappedBill = new DisplayBillDto {BillNumber = bill.BillNumber, PaymentMethod = bill.PaymentMethod, CreditCardNumber = bill.CreditCardNumber };

			_mapperMock.Setup(m => m.Map<DisplayBillDto>(bill)).Returns(mappedBill);
			_billRepoMock.Setup(b => b.GetBillByBillNumberAsync(billNumber)).ReturnsAsync(bill);

			//Act
			var result = await _sut.GetBillByBillNumberAsync(billNumber);

			//Assert
			Assert.IsType<DisplayBillDto>(result);
			Assert.Equal(result, mappedBill);
		}
		[Fact]
		public async Task BillService_GetBillByNumberNoTracking_ReturnsBill()
		{
			//Arrange
			var billNumber = "123";
			var bill = new Bill { BillNumber = billNumber, CreditCardNumber = "", PaymentMethod = "" };

			_billRepoMock.Setup(b => b.GetBillByBillNumberAsNoTracking(billNumber)).ReturnsAsync(bill);

			//Act
			var result = await _sut.GetBillByBillNumberAsNoTracking(billNumber);

			//Assert
			Assert.IsType<Bill>(result);
			Assert.Equal(result, bill);
		}

		[Fact]
		public async Task BillService_AddProduct_ReturnsTrue()
		{
			//Arrange
			AddBillDto addBillDto = new AddBillDto { BillNumber = "", CreditCardNumber = "", PaymentMethod = "" };
			Bill billMapped = new Bill
			{
				BillNumber = addBillDto.BillNumber,
				CreditCardNumber = addBillDto.CreditCardNumber,
				PaymentMethod = addBillDto.PaymentMethod
			};
			_mapperMock.Setup(m => m.Map<Bill>(addBillDto)).Returns(billMapped);
			_billRepoMock.Setup(r => r.AddBill(It.IsAny<Bill>())).Returns(true);

			//Act
			var result = _sut.AddBill(addBillDto);

			//Assert
			Assert.True(result);
			_billRepoMock.Verify(r => r.AddBill(It.IsAny<Bill>()), Times.Once);
		}
		[Fact]
		public async Task BillService_DeleteBill_ReturnsTrue()
		{
			//Arrange
			Bill bill = new Bill
			{
				BillNumber = "test123",
				CreditCardNumber = "test123",
				PaymentMethod = "test123"
			};
			_billRepoMock.Setup(r => r.DeleteBill(It.IsAny<Bill>())).Returns(true);

			//Act
			var result = _sut.DeleteBill(bill);

			//Assert
			Assert.True(result);
			_billRepoMock.Verify(r => r.DeleteBill(It.IsAny<Bill>()), Times.Once);
		}
		[Fact]
		public async Task BillService_UpdateBill_ReturnsTrue()
		{
			//Arrange
			string billNumber = "test123";
			AddBillDto billDto = new AddBillDto
			{
				BillNumber = "test123",
				CreditCardNumber = "test123",
				PaymentMethod = "test123"
			};
			var billMapped = new Bill { BillNumber = billNumber, CreditCardNumber = billDto.CreditCardNumber, PaymentMethod = billDto.PaymentMethod };

			_billRepoMock.Setup(r => r.UpdateBill(It.IsAny<Bill>())).Returns(true);
			_mapperMock.Setup(m => m.Map<Bill>(billDto)).Returns(billMapped);

			//Act
			var result = _sut.UpdateBill(billNumber, billDto);

			//Assert
			Assert.True(result);
			_billRepoMock.Verify(r => r.UpdateBill(It.IsAny<Bill>()), Times.Once);
		}
		[Fact]
		public async Task BillService_BillExists_ReturnsTrue()
		{
			var billNumber = "12345";
			List<Bill> bills = new List<Bill> { new Bill { BillNumber = billNumber } };

			//_billRepoMock.Setup(b => b.BillExists).return

		}
	}
}
