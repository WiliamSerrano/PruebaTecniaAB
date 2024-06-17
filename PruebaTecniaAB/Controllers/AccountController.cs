using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecniaAB.Models;
using PruebaTecniaAB.Models.ViewModels;

namespace PruebaTecniaAB.Controllers
{
    public class AccountController : Controller
    {

        private readonly DBVENTASContext _dbVentasContext;

        public AccountController(DBVENTASContext context)
        {

            _dbVentasContext = context;

        }

        [Authorize(policy: "AmdinAndAccountant")]
        public async Task<IActionResult> Index()
        {
            var sales = await _dbVentasContext.Sales.ToListAsync();

            return View(sales);
        }


        [HttpGet]
        public IActionResult UpdateSale(int idSale)
        {

            Sale oSale = new Sale();

            if (idSale != 0)
            {

                oSale = _dbVentasContext.Sales.Find(idSale);
            }

            return View(oSale);
        }

        [HttpPost]
        public IActionResult UpdateSale(Sale oSale)
        {

            oSale.PaidDate = DateTime.Now;
            oSale.IsPaid = true;

            _dbVentasContext.Sales.Update(oSale);
            _dbVentasContext.SaveChanges();

            return RedirectToAction("Index", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> ShowDetailSale(int idSale)
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

            SaleDetailVM vm = new SaleDetailVM() { 
            
                oSale = sale,
                oProducts = products
            
            };
            


            return View(vm);

        }
    }
}
