using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Diagnostics;
using WebApplication1.Controllers;
using WebApplication1.Enums;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Test.UnitTests;

    public class ProductsController_Tests
    {
        private readonly Mock<IProductService> _mockProductService;
        private readonly ProductsController _controller;

        public ProductsController_Tests()
        {
            _mockProductService = new Mock<IProductService>();
            _controller = new ProductsController(_mockProductService.Object);
        }

        [Fact]
        public async Task CreateAsync_Should_ReturnBadRequest_When_ModelStateIsInvalid()


    {
        // Arrange
        var schema = new ProductSchema();
            _controller.ModelState.AddModelError("Name", "Name is required");

            //Act
            var result = await _controller.Create(schema);
            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    [Fact]
    public async Task CreateAsync_Should_ReturnInternalServerError_When_ErrorOccurs()
    {
        // Arrange
        var schema = new ProductSchema();
        var request = new ServiceRequest<ProductSchema> { Content = schema };
        var response = new ServiceResponse<Product>
        {
            StatusCode = StatusCode.Conflict,
            Content = null
        };
        _mockProductService.Setup(x => x.GreateAsync(request)).ReturnsAsync(response);

        //Act
        var result = await _controller.Create(schema);

        // Assert
        Assert.IsType<ObjectResult>(result);

        var objectResult = result as ObjectResult;
        Assert.Equal(500, (int)objectResult!.StatusCode!);

    }





    [Fact]
    public async Task CreateAsync_Should_ReturnStatusCode500_When_ProductAlreadyExists()
    {
     
        // Arrange
        var schema = new ProductSchema();
        var request = new ServiceRequest<ProductSchema> { Content = schema };
        var response = new ServiceResponse<Product>
        {
            StatusCode = StatusCode.Conflict,
            Content = null
        };
        _mockProductService.Setup(x => x.GreateAsync(request)).ReturnsAsync(response);


        //Act
        var result = await _controller.Create(schema);
        var objectResult = result as ObjectResult; 

        // Assert
        Assert.NotNull(objectResult);

        var _response = objectResult.Value as ServiceResponse<Product>;
        Debug.WriteLine(value: _response.StatusCode);

        Assert.NotNull(_response);
        Assert.Equal(StatusCode.Conflict, _response.StatusCode);
    }
}

