using BAL.Services;
using Microsoft.AspNetCore.Mvc;

namespace APP.Controllers
{
    public class NotificationController : Controller
    {
        NotificationService notificationService;

        public NotificationController(NotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(HttpContext.Session.GetString("UserId")!);
            var data = notificationService.GetByUser(userId);
            notificationService.MarkAllAsRead(userId);
            return View(data);
        }

        public IActionResult MarkRead(int id)
        {
            notificationService.MarkAsRead(id);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            notificationService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}