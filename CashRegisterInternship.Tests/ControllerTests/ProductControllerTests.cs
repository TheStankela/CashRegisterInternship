using CashRegister.API.Controllers;
using CashRegister.API.Dto;
using CashRegister.API.Mediator.Commands.ProductCommands;
using CashRegister.API.Mediator.Querries.ProductQuerries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CashRegisterInternship.Tests.ControllerTests
{
	public class ProductControllerTests
	{
		private readonly Mock<IMediator> _mediatorMock = new Mock<IMediator>();
		private readonly ProductController _sut;


		public ProductControllerTests()
		{
			_sut = new ProductController(_mediatorMock.Object);
		}
		[Fact]
		public async Task ProductController_GetAllProducts_ShouldReturnOK()
		{
			List<ProductDto> expected = new List<ProductDto> { new ProductDto { Id = 1, Name = "FakeName", Price = 10 } };
			var querry = new Mock<GetAllProductsQuerry>();
			_mediatorMock
					.Setup(s => s.Send(It.IsAny<GetAllProductsQuerry>(), It.IsAny<CancellationToken>()))
					.ReturnsAsync(expected);

			//Act
			var result = await _sut.GetAllProducts();
			var okResult = result as ObjectResult;

			//Assert
			Assert.NotNull(okResult);
			Assert.True(okResult is OkObjectResult);
			Assert.IsType<List<ProductDto>>(okResult.Value);
			Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
		}
		[Fact]
		public async Task ProductController_GetProductById_ShouldReturnOK()
		{
			ProductDto expected = new ProductDto { Id = 1, Name = "FakeName", Price = 10 };
			var querry = new Mock<GetProductByIdQuerry>();
			_mediatorMock
					.Setup(s => s.Send(It.IsAny<GetProductByIdQuerry>(), It.IsAny<CancellationToken>()))
					.ReturnsAsync(expected);
			//Act
			var result = await _sut.GetProductByIdAsync(1);
			var okResult = result as ObjectResult;

			//Assert
			Assert.NotNull(okResult);
			Assert.True(okResult is OkObjectResult);
			Assert.IsType<ProductDto>(okResult.Value);
			Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
		}
		[Fact]
		public async Task ProductController_AddProduct_ShouldReturnTrueOK()
		{
			ProductDto productDto = new ProductDto { Id = 1, Name = "FakeName", Price = 10 };
			var querry = new Mock<AddProductCommand>();
			_mediatorMock
					.Setup(s => s.Send(It.IsAny<AddProductCommand>(), It.IsAny<CancellationToken>()))
					.ReturnsAsync(true);
			//Act
			var result = await _sut.AddProduct(productDto);
			var okResult = result as ObjectResult;

			//Assert
			Assert.NotNull(okResult);
			Assert.IsType<bool>(okResult.Value);
			Assert.IsType<ObjectResult>(okResult);
			Assert.Equal(true, okResult.Value);
			Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
		}
		[Fact]
		public async Task ProductController_UpdateProduct_ShouldReturnOK()
		{
			int productId = 1;
			ProductDto productDto = new ProductDto { Id = 1, Name = "FakeName", Price = 10 };
			var querry = new Mock<UpdateProductCommand>();
			_mediatorMock
					.Setup(s => s.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()))
					.ReturnsAsync(true);

			//Act
			var result = await _sut.UpdateProduct(productId, productDto);
			var okResult = result as ObjectResult;

			//Assert
			Assert.NotNull(okResult);
			Assert.IsType<bool>(okResult.Value);
			Assert.IsType<ObjectResult>(okResult);
			Assert.Equal(true, okResult.Value);
			Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
		}
		[Fact]
		public async Task ProductController_DeleteProduct_ShouldReturnOKTrue()
		{
			//Arrange
			int productId = 1;

			var querry = new Mock<DeleteProductCommand>();

			_mediatorMock
					.Setup(s => s.Send(It.IsAny<DeleteProductCommand>(), It.IsAny<CancellationToken>()))
					.ReturnsAsync(true);

			//Act
			var result = await _sut.DeleteProduct(productId);
			var okResult = result as ObjectResult;

			//Assert
			Assert.NotNull(okResult);
			Assert.IsType<bool>(okResult.Value);
			Assert.IsType<ObjectResult>(okResult);
			Assert.Equal(true, okResult.Value);
			Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
		}
	}
}