using BAL.DTOs;
using BAL.Services;
using Microsoft.AspNetCore.Mvc;

public class CustomerDashboardController : Controller
{
    UserService userService;

    public CustomerDashboardController(UserService userService)
    {
        this.userService = userService;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("UserName") != null)
        {
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            // Pass TempData success message to view
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
        // Guard: must be logged in
        if (HttpContext.Session.GetString("UserName") == null)
            return RedirectToAction("Login", "Account");

        // Get ID from session — customer can only edit their own profile
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
                // Update session name in case they changed it
                HttpContext.Session.SetString("UserName", u.Name);
                TempData["Success"] = "Profile updated successfully!";
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Update failed. Please try again.";
        }
        return View(u);
    }
}