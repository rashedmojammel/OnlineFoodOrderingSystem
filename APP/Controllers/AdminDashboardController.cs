
using Microsoft.AspNetCore.Mvc;

namespace APP.Controllers
{
    public class AdminDashboardController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                ViewBag.UserName = HttpContext.Session.GetString("UserName");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
    }
}