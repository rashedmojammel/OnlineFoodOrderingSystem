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
                return RedirectToAction("RedirectToDashboard");
            }
            else
            {
                ViewBag.Message = "Invalid credentials";
            }
            return View(dto);
        }

        public IActionResult RedirectToDashboard()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role == "Admin")
                return RedirectToAction("Index", "AdminDashboard");
            else if (role == "Customer")
                return RedirectToAction("Index", "CustomerDashboard");
            else
                return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Login");
            }
            return RedirectToAction("Login");
        }
    }
}