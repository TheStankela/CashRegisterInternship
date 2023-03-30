using AutoMapper;
using CashRegister.API.Controllers;
using CashRegister.API.Dto;
using CashRegister.Application.Services;
using CashRegister.Domain.Models;
using CashRegister.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CashRegisterInternship.Tests.ControllerTests
{
    public class ProductControllerTests
    {
        private readonly Mock<IProductService> _productServiceMock = new Mock<IProductService>();
        private readonly ProductController _sut;


        public ProductControllerTests()
        {
            _sut = new ProductController(_productServiceMock.Object);
        }
        [Fact]
        public async Task ProductController_GetAllProducts_ShouldReturnOK()
        {
            //Arrange
            var expectedResult = Mock.Of<List<Product>>();
            _productServiceMock.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(expectedResult);

            //Act
            var result = await _sut.GetAllProducts();

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));
        }
        [Fact]
        public async Task ProductController_GetProductById_ShouldReturnOK()
        {
            //Arrange
            var id = Mock.Of<Product>().Id;
            var expectedResult = Mock.Of<Product>();
            _productServiceMock.Setup(x => x.GetProductByIdAsync(id)).ReturnsAsync(expectedResult);

            //Act
            var result = await _sut.GetProductByIdAsync(id);

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));
        }
		[Fact]
		public void ProductController_AddProduct_ShouldReturnOK()
		{
			//Arrange
			var enteredProductDto = Mock.Of<ProductDto>();
            var mappedProduct = Mock.Of<Product>();

			//_productRepoMock.Setup(x => x.AddProduct(mappedProduct)).Returns(true);

			//Act
			var result = _sut.AddProduct(enteredProductDto);

			//Assert
			result.Should().BeOfType(typeof(OkObjectResult));
		}
	}
}