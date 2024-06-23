using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecniaAB.Data.Interfaces;
using PruebaTecniaAB.Data.Repositories;
using PruebaTecniaAB.Models;
using PruebaTecniaAB.Models.ViewModels;

namespace PruebaTecniaAB.Controllers
{
    public class AccountController : Controller
    {

        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {

            _accountRepository = accountRepository;

        }

        [Authorize(policy: "AmdinAndAccountant")]
        public async Task<IActionResult> Index()
        {
            var sales = await _accountRepository.GetSales();
            return View(sales);
        }


        [HttpGet]
        public async Task<IActionResult> UpdateSale(int idSale)
        {

            Sale oSale = new Sale();

            if (idSale != 0)
            {

                oSale = await _accountRepository.GetSaleById(idSale);
            }

            return View(oSale);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSale(Sale oSale)
        {

            await _accountRepository.UpdateSale(oSale);

            return RedirectToAction("Index", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> ShowDetailSale(int idSale)
        {
            var details = await _accountRepository.ShowSaleDetail(idSale);
            return View(details); 
            

        }
    }
}
