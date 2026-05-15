using BAL.Services;
using Microsoft.AspNetCore.Mvc;

namespace APP.Controllers
{
    public class MenuController : Controller
    {
        FoodService foodService;
        CategoryService categoryService;

        public MenuController(FoodService foodService,
                              CategoryService categoryService)
        {
            this.foodService = foodService;
            this.categoryService = categoryService;
        }

        public IActionResult Index(int? categoryId, string? search)
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToAction("Login", "Account");

            var categories = categoryService.Get();
            ViewBag.Categories = categories;
            ViewBag.SelectedCategory = categoryId;
            ViewBag.Search = search;

            var foods = string.IsNullOrEmpty(search)
                ? (categoryId.HasValue
                    ? foodService.GetByCategory(categoryId.Value)
                    : foodService.Get())
                : foodService.Search(search);

            return View(foods);
        }
    }
}