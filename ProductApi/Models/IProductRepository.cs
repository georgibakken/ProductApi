using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductApi.Models
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(long id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task AddProductAsync(Product item); 
    }
}