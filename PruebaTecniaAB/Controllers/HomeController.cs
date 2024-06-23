using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecniaAB.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace PruebaTecniaAB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize(policy: "AllRoles")]
        public IActionResult Index()
        {

            return View();
        }


    }
}
