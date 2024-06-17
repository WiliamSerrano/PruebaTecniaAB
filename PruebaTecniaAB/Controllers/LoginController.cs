using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PruebaTecniaAB.Models;
using System.Security.Claims;
using PruebaTecniaAB.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace PruebaTecniaAB.Controllers
{

    public class LoginController : Controller
    {
        private readonly DBVENTASContext _dbVentasContext;

        public LoginController(DBVENTASContext context)
        {

            _dbVentasContext = context;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var user = await _dbVentasContext.Users.SingleOrDefaultAsync(u => u.FirstName == loginVM.FirstName);

            if (user == null || loginVM.Clave != user.Password)
            {
                ModelState.AddModelError(string.Empty, "Incorrect login data");
                return RedirectToAction("LoginError"); 
               
            }

            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.Name, user.FirstName),
                 new Claim(ClaimTypes.Role, user.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        public IActionResult LoginError()
        {

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            return RedirectToAction("Index", "Login");
        }

    }
}
