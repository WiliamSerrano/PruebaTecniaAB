using Microsoft.EntityFrameworkCore;
using PruebaTecniaAB.Data.Interfaces;
using PruebaTecniaAB.Models;

namespace PruebaTecniaAB.Data.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly DBVENTASContext _dbVentasContext;

        public HomeRepository(DBVENTASContext context)
        {

            _dbVentasContext = context;

        }
        public async Task<IEnumerable<Product>> GetLowStocProduct(int quantity)
        {
            return await _dbVentasContext.Products.Where(p => p.Quantity <= quantity).ToListAsync();
        }
    }
}
