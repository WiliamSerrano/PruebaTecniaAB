using Microsoft.AspNetCore.Mvc;
using PruebaTecniaAB.Models;

namespace PruebaTecniaAB.Data.Interfaces
{
    public interface ISaleRepository
    {

        Task<IEnumerable<Product>> GetProductsToSale();

        Task<JsonResult> CreateSaleAsync(Sale oSale);
    }
}
