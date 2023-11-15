using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductSchema schema)
        {
            try 
            { 
            if (!ModelState.IsValid)            
                return BadRequest();

            var request = new ServiceRequest<ProductSchema> { Content = schema };   
            var response = await _productService.GreateAsync(request);
            return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex) 
            {
               Debug.WriteLine(ex.Message);
                return Problem(); 
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _productService.GetAllAsync();
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Problem();
            }
        }
    }
}

