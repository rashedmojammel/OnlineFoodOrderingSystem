using BAL.Services;
using Microsoft.AspNetCore.Mvc;

namespace APP.Controllers
{
    public class OrderController : Controller
    {
        OrderService orderService;

        public OrderController(OrderService orderService)
        {
            this.orderService = orderService;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserRole")?.ToLower() != "admin")
                return RedirectToAction("Login", "Account");

            var orders = orderService.GetAllOrders();
            return View(orders);
        }

   
        public IActionResult MyOrders()
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(HttpContext.Session.GetString("UserId")!);
            var orders = orderService.GetMyOrders(userId);
            return View(orders);
        }

     
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToAction("Login", "Account");

            var order = orderService.GetOrder(id);
            if (order == null) return NotFound();
            return View(order);
        }

   
        [HttpPost]
        public IActionResult UpdateStatus(int id, string status)
        {
            if (HttpContext.Session.GetString("UserRole")?.ToLower() != "admin")
                return RedirectToAction("Login", "Account");

            orderService.UpdateStatus(id, status);
            TempData["Success"] = "Order status updated!";
            return RedirectToAction("Index");
        }
    }
}