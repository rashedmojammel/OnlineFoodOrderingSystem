
using BAL.DTOs;
using BAL.Services;
using Microsoft.AspNetCore.Mvc;

namespace APP.Controllers
{
    public class AccountController : Controller
    {
        UserService userService;

        public AccountController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginDTO());
        }

        [HttpPost]
        public IActionResult Login(LoginDTO dto)
        {
            var user = userService.Login(dto.Name, dto.Role);
            if (user == null)
            {
                ViewBag.Error = "Invalid credentials";
                return View(dto);
            }

            HttpContext.Session.SetString("UserName", user.Name);
            HttpContext.Session.SetString("UserRole", user.Role);

            return user.Role switch
            {
                "Admin" => RedirectToAction("Index", "AdminDashboardController"),
                "Customer" => RedirectToAction("Index", "CustomerDashboardController"),
                _ => RedirectToAction("Login")
            };
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}