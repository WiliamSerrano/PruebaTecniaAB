using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecniaAB.Controllers;
using PruebaTecniaAB.Data.Interfaces;
using PruebaTecniaAB.Models;

namespace PruebaTecniaAB.Data.Repositories
{
    public class SaleRepository : ISaleRepository
    {

        private readonly DBVENTASContext _dbVentasContext;

        public SaleRepository(DBVENTASContext context)
        {

            _dbVentasContext = context;

        }

        public async Task<JsonResult> CreateSaleAsync(Sale oSale)
        {
            Sale newSale = new Sale();

            if (oSale != null)
            {

                newSale.NameClient = oSale.NameClient;
                newSale.Description = oSale.Description;
                newSale.Mail = oSale.Mail;
                newSale.TotalPrice = oSale.TotalPrice;
                newSale.CreationDate = DateTime.Now;
                newSale.PaidDate = DateTime.Now;
                newSale.IsPaid = false;

                _dbVentasContext.Sales.Add(newSale);
                await _dbVentasContext.SaveChangesAsync();
            }
            else
            {
                return new JsonResult(new { error = "Invalid sale data" });
            }

            var idSale = newSale.IdSale;

            if (oSale.SalesProducts.Count != 0)
            {
                foreach (var product in oSale.SalesProducts)
                {
                    SalesProduct saleDetail = new SalesProduct();

                    saleDetail.IdSale = idSale;
                    saleDetail.IdProduct = product.IdProduct;

                    _dbVentasContext.SalesProducts.Add(saleDetail);
                    await _dbVentasContext.SaveChangesAsync();
                }

                List<int> listprds = new List<int>();

                foreach (var product in oSale.SalesProducts)
                {

                    listprds.Add(product.IdProduct);

                }

                var idGrouped = listprds.GroupBy(x => x).Select(x => new ProductGroup { IdProduct = x.Key, Quantity = x.Count() }).ToList();

                foreach (var id in idGrouped)
                {

                    var udProduct = _dbVentasContext.Products.Where(x => x.IdProduct == id.IdProduct).FirstOrDefault();
                    udProduct.Quantity -= id.Quantity;
                    _dbVentasContext.Products.Update(udProduct);
                    await _dbVentasContext.SaveChangesAsync();

                }

            }

            return new JsonResult(true);
        }

        public class ProductGroup{ 
    
        public int IdProduct { get; set;}
        public int Quantity { get; set; }
        
    
        }

        public async Task<IEnumerable<Product>> GetProductsToSale()
        {
            return await _dbVentasContext.Products.ToListAsync();
        }


    }
}
