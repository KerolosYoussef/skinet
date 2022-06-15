using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    
    public class ProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.
                            Include(p => p.ProductBrand)
                            .Include(p => p.ProductType)
                            .SingleOrDefaultAsync(p=>p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> ListAllAsync()
        {
            return await _context.Products
                        .Include(p=>p.ProductBrand)
                        .Include(p => p.ProductType)
                        .ToListAsync();
        }
    }
}