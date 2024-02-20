using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infraestructure.Persistences.Contexts;
using POS.Infraestructure.Persistences.Interfaces;
using System.Linq.Dynamic.Core;

namespace POS.Infraestructure.Persistences.Repositories
{
    public class PurcharseDetailRepository : IPurcharseDetailRepository
    {
        private readonly POSContext _context;

        public PurcharseDetailRepository(POSContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PurcharseDetail>> GetPurcharseDetailByPurcharseId(int purcharseId)
        {
            var response = await _context.Products
                .AsNoTracking() //Los resultados no deben ser rastreados
                .Join(_context.PurcharseDetails, p => p.Id, pd => pd.ProductId, (p, pd)
                    => new { Product = p, PurcharseDetail = pd }
                ).Where(x => x.PurcharseDetail.PurcharseId == purcharseId)
                .Select(x => new PurcharseDetail
                {
                    ProductId = x.Product.Id,
                    Product = new Product
                    {
                        Image = x.Product.Image,
                        Code = x.Product.Code,
                        Name = x.Product.Name
                    },
                    Quantity = x.PurcharseDetail.Quantity,
                    UnitPurcharsePrice = x.PurcharseDetail.UnitPurcharsePrice,
                    Total = x.PurcharseDetail.Total
                })
                .ToListAsync();

            return response;
           
        }
    }
}
