using WebApplication1.Enums;

namespace WebApplication1.Models
{
    public class ServiceResponse<T>
    {
        public StatusCode StatusCode { get; set; } 
        public T? Content{ get; set; }
    }
}
