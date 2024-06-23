using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using PruebaTecniaAB.Data.Interfaces;
using PruebaTecniaAB.Data.Repositories;
using PruebaTecniaAB.Models;

namespace PruebaTecniaAB.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {

            _productRepository = productRepository;

        }

        [Authorize(policy: "AdminOnly")]
        public async Task<IActionResult> Index()
        {

            var products = await _productRepository.GetProducts();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Product_Detail(int idProduct)
        {

            Product oProduct = new Product();

            if (idProduct != 0)
            {

                oProduct = await _productRepository.GetProductById(idProduct);

            }

            return View(oProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Product_Detail(Product oProduct)
        {

            if (ModelState.IsValid)
            {
                if (oProduct.IdProduct == 0)
                {
                    await _productRepository.AddProduct(oProduct);

                }
                else
                {
                    await _productRepository.UpdateProduct(oProduct);
                }

                return RedirectToAction("Index", "Product");

            }
           

            return View(oProduct);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int idProduct)
        {

            Product oProduct = await _productRepository.GetProductToDelete(idProduct);

            return View(oProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Product oProduct)
        {

            await _productRepository.DeleteProduct(oProduct);

            return RedirectToAction("Index", "Product");
        }
    }
}
