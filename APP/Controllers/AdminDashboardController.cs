
using Microsoft.AspNetCore.Mvc;

namespace APP.Controllers
{
    public class AdminDashboardController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToAction("Login", "Account");

            return View();
        }
    }
}