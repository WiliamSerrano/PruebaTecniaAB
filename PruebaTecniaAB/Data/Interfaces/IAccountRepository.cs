using PruebaTecniaAB.Models;
using PruebaTecniaAB.Models.ViewModels;

namespace PruebaTecniaAB.Data.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Sale>> GetSales();

        Task<Sale> GetSaleById(int id);

        Task<Sale> UpdateSale(Sale sale);

        Task<SaleDetailVM> ShowSaleDetail(int id);
    }
}
