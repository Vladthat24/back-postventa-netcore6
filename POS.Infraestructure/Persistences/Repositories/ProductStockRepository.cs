using POS.Domain.Entities;
using POS.Infraestructure.Persistences.Contexts;
using POS.Infraestructure.Persistences.Interfaces;

namespace POS.Infraestructure.Persistences.Repositories
{
    public class ProductStockRepository : IProductStockRepository
    {
        private readonly POSContext _context;

        public ProductStockRepository(POSContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterProductStock(ProductStock productStock)
        {
            await _context.AddAsync(productStock);
            var recordsAffected= await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }
    }
}
