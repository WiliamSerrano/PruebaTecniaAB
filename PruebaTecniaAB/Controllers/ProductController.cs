using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecniaAB.Models;

namespace PruebaTecniaAB.Controllers
{
    public class ProductController : Controller
    {

        private readonly DBVENTASContext _dbVentasContext;

        public ProductController(DBVENTASContext context)
        {

            _dbVentasContext = context;

        }

        [Authorize(policy: "AdminOnly")]
        public async Task<IActionResult> Index()
        {

            List<Product> productList =  await _dbVentasContext.Products.ToListAsync();

            return View(productList);
        }

        [HttpGet]
        public IActionResult Product_Detail(int idProduct)
        {

            Product oProduct = new Product();

            if (idProduct != 0)
            {

                oProduct = _dbVentasContext.Products.Find(idProduct);

            }

            return View(oProduct);
        }

        [HttpPost]
        public IActionResult Product_Detail(Product oProduct)
        {
            if (oProduct.IdProduct == 0)
            {

                _dbVentasContext.Products.Add(oProduct);

            }
            else
            {

                _dbVentasContext.Products.Update(oProduct);

            }

            _dbVentasContext.SaveChanges();

            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public IActionResult Delete(int idProduct)
        {

            Product oProduct = _dbVentasContext.Products.Where(e => e.IdProduct == idProduct).FirstOrDefault();

            return View(oProduct);
        }

        [HttpPost]
        public IActionResult Delete(Product oProduct)
        {

            _dbVentasContext.Remove(oProduct);
            _dbVentasContext.SaveChanges();

            return RedirectToAction("Index", "Product");
        }
    }
}
