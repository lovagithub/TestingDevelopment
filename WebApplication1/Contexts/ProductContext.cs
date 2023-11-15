using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Contexts
{
    public class ProductContext : DbContext
    {
       
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
            {
            }
        public DbSet<ProductEntity> Products { get; set; }  
    }


}
