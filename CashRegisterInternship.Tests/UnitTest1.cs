using AutoMapper;
using CashRegister.API.Controllers;
using CashRegister.Infrastructure.Repositories;
using Moq;
using System.Runtime.CompilerServices;

namespace CashRegisterInternship.Tests
{
	public class UnitTest1
	{
		private readonly Mock<IProductRepository> _productRepoMock = new Mock<IProductRepository>();
		private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
		private readonly ProductController _sut;


		public UnitTest1()
        {
			_sut = new ProductController(_productRepoMock.Object, _mapperMock.Object);
        }
        [Fact]
		public void ProductController_GetAllProducts_ShouldReturnListOfProducts()
		{
			//Arrange
			
			//Act

			//Assert
		}
	}
}