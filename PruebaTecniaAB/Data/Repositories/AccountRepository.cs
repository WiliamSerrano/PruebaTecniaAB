using Microsoft.EntityFrameworkCore;
using PruebaTecniaAB.Data.Interfaces;
using PruebaTecniaAB.Models;
using PruebaTecniaAB.Models.ViewModels;

namespace PruebaTecniaAB.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {

        private readonly DBVENTASContext _dbVentasContext;

        public AccountRepository(DBVENTASContext context)
        {

            _dbVentasContext = context;

        }

        public async Task<Sale> GetSaleById(int idSale)
        {
            Sale oSale = new Sale();

            if (idSale != 0)
            {

                oSale = await _dbVentasContext.Sales.FindAsync(idSale);

            }

            return oSale;
        }

        public async Task<IEnumerable<Sale>> GetSales()
        {
            var sales = await _dbVentasContext.Sales.ToListAsync();

            return sales;
        }

        public async Task<SaleDetailVM> ShowSaleDetail(int idSale)
        {
            var sale = _dbVentasContext.Sales.Where(m => m.IdSale == idSale).FirstOrDefault();
            var products = await _dbVentasContext.SalesProducts.Where(x => x.IdSale == sale.IdSale).Include(c => c.oProduct)
            .GroupBy(x => x.IdProduct).Select(p => new Product
            {
                ProductName = p.Select(x => x.oProduct.ProductName).FirstOrDefault(),
                Quantity = _dbVentasContext.SalesProducts
                                .Where(x => x.IdProduct == p.Select(x => x.oProduct.IdProduct).FirstOrDefault() && x.IdSale == sale.IdSale).Count(),
                UnitPrice = p.Select(x => x.oProduct.UnitPrice).FirstOrDefault(),

            }).ToListAsync();

            SaleDetailVM vm = new SaleDetailVM()
            {

                oSale = sale,
                oProducts = products

            };

            return vm;
        }

        public async Task<Sale> UpdateSale(Sale sale)
        {
            sale.PaidDate = DateTime.Now;
            sale.IsPaid = true;

            _dbVentasContext.Sales.Update(sale);
            await _dbVentasContext.SaveChangesAsync();

            return sale;
        }
    }
}
