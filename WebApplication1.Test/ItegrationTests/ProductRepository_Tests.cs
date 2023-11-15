using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using WebApplication1.Contexts;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Test.ItegrationTests;

public class ProductRepository_Tests
{  
    private readonly ProductContext _context;
    private readonly IProductRepository _repository;
    public ProductRepository_Tests()
    {
       var option= new DbContextOptionsBuilder<ProductContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;
        _context = new ProductContext(option);
        _repository = new ProductRepository(_context);
    }
    [Fact]
    public async Task CreateAsync_Should_AddEntityToDatabase_And_ReturnEntity()
    {
        // Arrange
        var entity = new ProductEntity { Name = "Test Product" };

        // Act
        var result = await _repository.CreteAsync(entity);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<ProductEntity>(result);
        Assert.Equal(entity.Name, result.Name);

    }
    [Fact]
    public async Task ExistAsync_Should_ReturnTrue_When_EntityAlreadyExist()
    {
        // Arrange
        var entity = new ProductEntity { Name = "Test Product" };
        await _repository.CreteAsync(entity);

        // Act
        var result = await _repository.ExistAsync(x => x.Name == entity.Name);

        // Assert
        Assert.True(result);
        await DisposeAsync(entity);

    }
    [Fact]
    public async Task ExistAsync_Should_ReturnFalse_When_EntityDoesNotExist()
    {
        // Arrange
        var entity = new ProductEntity { Name = "Test Product" };

        // Act
        var result = await _repository.ExistAsync(x => x.Name == entity.Name);

        // Assert
        Assert.False(result);
        await DisposeAsync(entity);

    }

    private async Task DisposeAsync(ProductEntity entity)
    {
        await _context.Database.EnsureDeletedAsync();
        _context.Dispose();
    }
}
