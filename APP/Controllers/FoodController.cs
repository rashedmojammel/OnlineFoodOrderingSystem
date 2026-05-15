using BAL.DTOs;
using BAL.Services;
using Microsoft.AspNetCore.Mvc;

namespace APP.Controllers
{
    public class FoodController : Controller
    {
        FoodService foodService;
        CategoryService categoryService;

        public FoodController(FoodService foodService,
                              CategoryService categoryService)
        {
            this.foodService = foodService;
            this.categoryService = categoryService;
        }

        public IActionResult Index(string? search)
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToAction("Login", "Account");

            List<FoodDTO> data;
            if (!string.IsNullOrEmpty(search))
            {
                data = foodService.Search(search);
                ViewBag.Search = search;
            }
            else
            {
                data = foodService.Get();
            }
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToAction("Login", "Account");

            ViewBag.Categories = categoryService.Get();
            return View(new FoodDTO());
        }

        [HttpPost]
        public IActionResult Create(FoodDTO dto)
        {
            if (ModelState.IsValid)
            {
                var res = foodService.Create(dto);
                if (res)
                {
                    TempData["Success"] = "Food item created successfully!";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Categories = categoryService.Get();
            ViewBag.Error = "Failed to create food item.";
            return View(dto);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToAction("Login", "Account");

            var data = foodService.Get(id);
            if (data == null) return NotFound();
            ViewBag.Categories = categoryService.Get();
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(FoodDTO dto)
        {
            if (ModelState.IsValid)
            {
                var res = foodService.Update(dto);
                if (res)
                {
                    TempData["Success"] = "Food item updated successfully!";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Categories = categoryService.Get();
            ViewBag.Error = "Failed to update food item.";
            return View(dto);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToAction("Login", "Account");

            var data = foodService.Get(id);
            if (data == null) return NotFound();
            return View(data);
        }

        [HttpPost]
        public IActionResult Delete(int id, string Decision)
        {
            if (Decision.Equals("Yes"))
            {
                var res = foodService.Delete(id);
                if (res)
                {
                    TempData["Success"] = "Food item deleted successfully!";
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}