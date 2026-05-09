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
            if (user != null)
            {
                HttpContext.Session.SetString("UserName", user.Name);
                HttpContext.Session.SetString("UserRole", user.Role);

                // ✅ Direct redirect, no intermediate action
                if (user.Role == "admin")
                    return RedirectToAction("Index", "AdminDashboard");
                else if (user.Role == "customer")
                    return RedirectToAction("Index", "CustomerDashboard");
            }

            ViewBag.Message = "Invalid credentials";
            return View(dto);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}