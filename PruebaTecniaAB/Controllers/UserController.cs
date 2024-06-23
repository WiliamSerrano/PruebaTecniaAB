using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecniaAB.Data.Interfaces;
using PruebaTecniaAB.Models;

namespace PruebaTecniaAB.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {

            _userRepository = userRepository;

        }

        [Authorize(policy: "AdminOnly")]
        public async Task<ViewResult> Index()
        {
            var users = await _userRepository.GetUsers();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> User_Detail(int idUser)
        {
           
            var user = await _userRepository.GetUserById(idUser);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> User_Detail(User oUser)
        {

            if (ModelState.IsValid)
            {
                if (oUser.IdUser == 0)
                {

                    await _userRepository.Add(oUser);

                }
                else
                {
                    await _userRepository.UpdateUser(oUser);
                }

                return RedirectToAction("Index", "User");

            }
            return View(oUser);
            
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int idUser)
        {

            User oUser = await _userRepository.GetUserToDelete(idUser);

            return View(oUser);
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(User oUser)
        {

            await _userRepository.DeleteUser(oUser);

            return RedirectToAction("Index", "User");
        }
    }
}
