using AutoMapper;
using CashRegister.API.Dto;
using CashRegister.Application.Services;
using CashRegister.Domain.Models;
using CashRegister.Infrastructure.Repositories;
using Moq;

namespace CashRegisterInternship.Tests.ServiceTests
{
	public class ProductServiceTests
	{
		private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
		private readonly Mock<IProductRepository> _productRepoMock = new Mock<IProductRepository>();
		private ProductService _sut;
		public ProductServiceTests()
		{
			_sut = new ProductService(_productRepoMock.Object, _mapperMock.Object);
		}
		[Fact]
		public async Task ProductService_GetAllProducts_ReturnsProductDtoList()
		{
			//Arrange
			var product = new Product { Id = 1, Name = "Test", Price = 10 };
			var productMapped = new ProductDto { Id = product.Id, Price = product.Price, Name = product.Name };

			var products = new List<Product> { product };
			var mappedProducts = new List<ProductDto> { productMapped };

			_mapperMock.Setup(m => m.Map<List<ProductDto>>(products)).Returns(mappedProducts);
			_productRepoMock.Setup(r => r.GetAllProductsAsync()).ReturnsAsync(products);

			//Act
			var result = await _sut.GetAllProductsAsync();

			//Assert
			Assert.NotNull(result);
			Assert.NotEmpty(result);
			Assert.IsType<List<ProductDto>>(result);
			Assert.Equal(mappedProducts, result);
		}
		[Fact]
		public async Task ProductService_GetProductById_ReturnsProductDto()
		{
			//Arrange
			var productId = 1;
			var product = new Product { Id = 1, Name = "Test", Price = 10 };
			var productMapped = new ProductDto { Id = product.Id, Price = product.Price, Name = product.Name };

			_mapperMock.Setup(m => m.Map<ProductDto>(product)).Returns(productMapped);
			_productRepoMock.Setup(p => p.GetProductByIdAsync(productId)).ReturnsAsync(product);

			//Act
			var result = await _sut.GetProductByIdAsync(productId);

			//Assert
			Assert.NotNull(result);
			Assert.Equal(productId, result.Id);
			Assert.Equal(productMapped, result);
		}
		[Fact]
		public async Task ProductService_AddProduct_ReturnsTrue()
		{
			//Arrange
			var productDto = new ProductDto { Id = 1, Name = "Test", Price = 10 };
			var productMapped = new Product { Id = productDto.Id, Price = productDto.Price, Name = productDto.Name };

			_mapperMock.Setup(m => m.Map<Product>(productDto)).Returns(productMapped);
			_productRepoMock.Setup(p => p.AddProduct(productMapped)).Returns(true);
			//Act
			var result = _sut.AddProduct(productDto);

			//Assert
			Assert.IsType<bool>(result);
			Assert.True(result);
		}
		[Fact]
		public async Task ProductService_UpdateProduct_ReturnsTrue()
		{
			//Arrange
			var productId = 1;
			var productDto = new ProductDto { Id = 1, Name = "Test", Price = 10 };
			var productMapped = new Product { Id = productDto.Id, Price = productDto.Price, Name = productDto.Name };
			_mapperMock.Setup(m => m.Map<Product>(productDto)).Returns(productMapped);
			_productRepoMock.Setup(c => c.UpdateProduct(productMapped)).Returns(true);
			_productRepoMock.Setup(c => c.ProductExists(productId)).Returns(true);

			//Act
			var result = _sut.UpdateProduct(productId, productDto);

			//Assert
			Assert.IsType<bool>(result);
			Assert.True(result);
		}
		[Fact]
		public async Task ProductService_DeleteProduct_ReturnsTrue()
		{
			//Arrange
			var productId = 1;
			var product = new Product { Id = 1, Name = "Test", Price = 10 };

			_productRepoMock.Setup(p => p.GetProductByIdAsync(productId)).ReturnsAsync(product);
			_productRepoMock.Setup(c => c.DeleteProduct(product)).Returns(true);

			//Act
			var result = await _sut.DeleteProduct(productId);

			//Assert
			Assert.Equal(productId, product.Id);
			Assert.IsType<bool>(result);
			Assert.True(result);
		}
	}
}
