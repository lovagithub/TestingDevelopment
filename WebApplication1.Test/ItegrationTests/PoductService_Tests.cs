using Moq;
using System.Linq.Expressions;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Services;

namespace WebApplication1.Test.ItegrationTests;

public class PoductService_Tests
{
    private readonly IProductService _productService;
    private readonly Mock<IProductRepository> _productRepositoryMock;

    public PoductService_Tests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _productService = new ProductService( _productRepositoryMock.Object );
    }
    [Fact]
    public async Task TaskAsync_Should_ReturnServiceResponceWithStatusCode201_When_CreatedSuccessfuy()
    {

        // Arrange
        var schema = new ProductSchema() { Name = "Product 1" };
        var request = new ServiceRequest<ProductSchema> { Content = schema };
        _productRepositoryMock.Setup(x => x.ExistAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>())).ReturnsAsync(false);

        // Act

        var result = await _productService.GreateAsync(request);
        // Assert
        Assert.NotNull(result);
        Assert.IsType<ServiceResponse<Product>>( result );
        Assert.Equal(201, (int)result.StatusCode );

    }
}
