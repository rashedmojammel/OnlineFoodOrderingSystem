using BAL.DTOs;
using BAL.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class UserController : Controller
    {
        UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Create()
        {
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
            }
            return View(u);
        }

        public IActionResult Index(string? search)
        {
            List<UserDTO> data;

            if (!string.IsNullOrEmpty(search))
            {
                data = userService.Search(search);
                ViewBag.Search = search; 
            }
            else
            {
                data = userService.Get();
            }

            return View(data);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = userService.Get(id);
            return View(data);
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
            }
            return View(u);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = userService.Get(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Delete(int id, string Decision)
        {
            if (Decision.Equals("Yes"))
            {
                var res = userService.Delete(id);
                if (res == true)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        //public IActionResult UserList()
        //{
        //    if (HttpContext.Session.GetString("UserName") == null)
        //        return RedirectToAction("Login", "Account");

        //    var users = userService.Get();
        //    return View(users);
        //}
    }
}