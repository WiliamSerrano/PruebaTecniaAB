using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaTecniaAB.Models;

namespace PruebaTecniaAB.Controllers
{
    public class UserController : Controller
    {

        private readonly DBVENTASContext _dbVentasContext;

        public UserController(DBVENTASContext context)
        {

            _dbVentasContext = context;

        }

        [Authorize(policy: "AdminOnly")]
        public async Task<IActionResult> Index()
        {
            List<User> userlist = await _dbVentasContext.Users.ToListAsync();
            return View(userlist);
        }

        [HttpGet]
        public IActionResult User_Detail(int idUser)
        {

            User oUser = new User();

            if ( idUser != 0)
            {

                oUser = _dbVentasContext.Users.Find(idUser);

            }

            return View(oUser);
        }

        [HttpPost]
        public IActionResult User_Detail(User oUser)
        {
            if (oUser.IdUser == 0)
            {

                _dbVentasContext.Users.Add(oUser);

            }
            else
            {

                _dbVentasContext.Users.Update(oUser);

            }

            _dbVentasContext.SaveChanges();

            return RedirectToAction("Index", "User");
        }

        [HttpGet]
        public IActionResult Eliminar(int idUser)
        {

            User oUser = _dbVentasContext.Users.Where(e => e.IdUser == idUser).FirstOrDefault();

            return View(oUser);
        }

        [HttpPost]
        public IActionResult Eliminar(User oUser)
        {

            _dbVentasContext.Remove(oUser);
            _dbVentasContext.SaveChanges();

            return RedirectToAction("Index", "User");
        }
    }
}
