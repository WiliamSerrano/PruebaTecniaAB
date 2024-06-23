using PruebaTecniaAB.Models;

namespace PruebaTecniaAB.Data.Interfaces
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Product>> GetLowStocProduct(int quantity);
    }
}
