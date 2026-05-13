
using BAL.DTOs;
using BAL.Services;
using Humanizer.DateTimeHumanizeStrategy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace APP.Controllers
{
    public class AdminDashboardController : Controller
    {

        UserService userService;
        public AdminDashboardController(UserService userService) {
            this.userService = userService;

        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserName") != null)
            {
                ViewBag.UserName = HttpContext.Session.GetString("UserName");
                ViewBag.TotalUsers = userService.Get().Count;
                //ViewBag.TotalCategories = 0; // replace when CategoryService is added
                //ViewBag.TotalFoods = 0; // replace when FoodService is added
                //ViewBag.TotalOrders = 0; // replace when OrderService is added

                ViewBag.RecentUsers = userService.Get().TakeLast(5).ToList();

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Users = userService.Get();
            return View(new UserDTO());
        }
        [HttpPost]
        public IActionResult Create(UserDTO u)
        {

            if (ModelState.IsValid)
            {
                var res = userService.Create(u);
                if (res == true)
                {

                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle creation failure (e.g., show an error message)
                    ModelState.AddModelError("", "Failed to create user.");
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = userService.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(UserDTO u)
        {
            if (ModelState.IsValid)
            {
                var res = userService.Update(u);
                if (res == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle update failure (e.g., show an error message)
                    ModelState.AddModelError("", "Failed to update user.");
                }
            }
            return View(u);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var user = userService.Get(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Delete(int id, string Decision)
        {
            if (Decision.Equals("Yes"))
            {
                var user = userService.Delete(id);
                if (user == true)
                {
                    return RedirectToAction("Index");
                }
                else

                    ModelState.AddModelError("", "Failed to delete user.");
            }
            return RedirectToAction("Index");
        }

        public IActionResult UserList()
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToAction("Login", "Account");

            var users = userService.Get();
            return View(users);
        }
    }
}