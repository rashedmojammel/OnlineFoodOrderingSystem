using BAL.DTOs;
using BAL.Services;
using Microsoft.AspNetCore.Mvc;

public class CustomerDashboardController : Controller
{
    UserService userService;
    OrderService orderService;

    public CustomerDashboardController(UserService userService, OrderService orderService)
    {
        this.userService = userService;
        this.orderService = orderService;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("UserName") != null)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.RecentOrders = orderService.GetAllOrders()
             
                                          .Take(5)
                                          .ToList();

        }
        else
        {
            return RedirectToAction("Login", "Account");
        }
        return View();
    }

    [HttpGet]
    public IActionResult Edit()
    {
  
        if (HttpContext.Session.GetString("UserName") == null)
            return RedirectToAction("Login", "Account");

        
        int userId = int.Parse(HttpContext.Session.GetString("UserId"));
        var data = userService.Get(userId);

        if (data == null) return NotFound();
        return View(data);
    }

    [HttpPost]
    public IActionResult Edit(UserDTO u)
    {
        if (HttpContext.Session.GetString("UserName") == null)
            return RedirectToAction("Login", "Account");

        if (ModelState.IsValid)
        {
            var res = userService.Update(u);
            if (res)
            {
                HttpContext.Session.SetString("UserName", u.Name);
                TempData["Success"] = "Profile updated successfully!";
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Update failed. Please try again.";
        }
        return View(u);
    }
}