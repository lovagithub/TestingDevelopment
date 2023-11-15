using System.Diagnostics;
using WebApplication1.Enums;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services;
public interface IProductService
{
    Task<ServiceResponse<Product>> GreateAsync(ServiceRequest<ProductSchema> request);
    Task<ServiceResponse<Product>> GetByArticleNumberAsync(int articleNumber);
    Task<ServiceResponse<List<Product>>> GetAllAsync();
}

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ServiceResponse<Product>> GreateAsync(ServiceRequest<ProductSchema> request)
    {
        var responce = new ServiceResponse<Product>();

        try
        {
            if (request.Content != null)
            {
                if (!await _productRepository.ExistAsync(entity => entity.Name == request.Content.Name))
                {
                    responce.Content = await _productRepository.CreteAsync(request.Content!);
                    responce.StatusCode = StatusCode.Created;
                }
                else
                {
                    responce.StatusCode = StatusCode.Conflict;
                    responce.Content = null;
                }
            }
            else
            {              
                responce.StatusCode = StatusCode.BadRequest;
                responce.Content = null;
            }
        }
        catch (Exception ex) 
        {
            Debug.WriteLine(ex.Message);
            responce.StatusCode = StatusCode.InternalServerError;
            responce.Content = null;
        }

        return responce;
    }

    public async Task<ServiceResponse<List<Product>>> GetAllAsync()
    {
        var responce = new ServiceResponse<List<Product>>
        {
            StatusCode = StatusCode.Ok,
            Content = new List<Product>()
        };
        
        try
        {
            var result = await _productRepository.ReadAllAsync();
            foreach (var entity in result)          
                responce.Content.Add(entity);           
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            responce.StatusCode = StatusCode.InternalServerError;
            _ = responce.Content = null;
        }

        return responce;
    }


    public async Task<ServiceResponse<Product>> GetByArticleNumberAsync(int articleNumber)
    {
        throw new NotImplementedException();
    } 
}


   

