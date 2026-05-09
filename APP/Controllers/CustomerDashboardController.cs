
using Microsoft.AspNetCore.Mvc;

namespace APP.Controllers
{
    public class CustomerDashboardController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "Customer")
                return RedirectToAction("Login", "Account");

            return View();
        }
    }
}