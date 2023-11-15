using WebApplication1.Contexts;
using WebApplication1.Models;


namespace WebApplication1.Repositories;
    public interface IProductRepository : IRepo<ProductEntity, ProductContext>
    {

    }
    public class ProductRepository : Repo<ProductEntity, ProductContext>, IProductRepository
    {
        public ProductRepository(ProductContext context) : base(context)
        { 
        }  
    }

