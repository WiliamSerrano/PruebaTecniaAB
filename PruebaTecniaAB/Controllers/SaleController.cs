using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecniaAB.Data.Interfaces;
using PruebaTecniaAB.Models;

namespace PruebaTecniaAB.Controllers
{
    public class SaleController : Controller
    {

        private readonly ISaleRepository _saleRepository;

        public SaleController(ISaleRepository saleRepository)
        {

            _saleRepository = saleRepository;

        }

        [Authorize(policy: "AmdinAndSeller")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Products_Data()
        {

            var data = await _saleRepository.GetProductsToSale();

            return Json(data);
        }

        [HttpPost]
        public async Task<JsonResult> createSale([FromBody] Sale oSale)
        {
            var result = await _saleRepository.CreateSaleAsync(oSale);
            return result;
        }

    }
}
