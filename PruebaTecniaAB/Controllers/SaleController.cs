using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecniaAB.Models;

namespace PruebaTecniaAB.Controllers
{
    public class SaleController : Controller
    {

        private readonly DBVENTASContext _dbVentasContext;

        public SaleController(DBVENTASContext context)
        {

            _dbVentasContext = context;

        }

        [Authorize(policy: "AmdinAndSeller")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Products_Data()
        {

            List<Product> data = await _dbVentasContext.Products.ToListAsync();

            return Json(data);
        }

        [HttpPost]
        public async Task<JsonResult> createSale([FromBody] Sale oSale)
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
                return Json(new { error = "error" });
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

                var idGrouped = listprds.GroupBy(x => x).Select(x => new ProductGroup { IdProduct = x.Key, Quantity = x.Count()}).ToList();
                
                foreach (var id in idGrouped)
                {

                    var udProduct = _dbVentasContext.Products.Where(x => x.IdProduct == id.IdProduct).FirstOrDefault();
                    udProduct.Quantity = id.Quantity;
                    _dbVentasContext.Products.Update(udProduct);
                    await _dbVentasContext.SaveChangesAsync();

                }

            }

            return Json(true);
        }

    }

    public class ProductGroup{ 
    
        public int IdProduct { get; set;}
        public int Quantity { get; set; }
        
    
    }
}
