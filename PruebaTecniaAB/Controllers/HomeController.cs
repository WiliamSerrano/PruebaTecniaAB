using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecniaAB.Models;
using System.Diagnostics;
using System.Security.Claims;
using PruebaTecniaAB.Data.Interfaces;

namespace PruebaTecniaAB.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeRepository _homeRepository;

        public HomeController(IHomeRepository homeRepository)
        {

            _homeRepository = homeRepository;

        }

        [Authorize(policy: "AllRoles")]
        public async Task<IActionResult> Index(int quantity = 5)
        {
            var lowProduct = await _homeRepository.GetLowStocProduct(quantity);
            return View(lowProduct);
        }


    }
}
