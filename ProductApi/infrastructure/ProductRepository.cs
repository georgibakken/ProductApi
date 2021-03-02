using Microsoft.EntityFrameworkCore;
using ProductApi.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProductApi.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _dbContext;

        public ProductRepository(ProductContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(long id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task AddProductAsync(Product item)
        {
            _dbContext.Add(item);
            await _dbContext.SaveChangesAsync();
        }        
    }
}