using Microsoft.AspNetCore.Mvc;
using BAL.DTOs;
using BAL.Services;
namespace APP.Controllers

{
    public class CategoryController : Controller
    {
        CategoryService categoryService;

        public CategoryController(CategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult Index(string? search)
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToAction("Login", "Account");

            List<CategoryDTO> data;
            if (!string.IsNullOrEmpty(search))
            {
                data = categoryService.Search(search);
                ViewBag.Search = search;
            }
            else
            {
                data = categoryService.Get();
            }
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToAction("Login", "Account");

            return View(new CategoryDTO());
        }

        [HttpPost]
        public IActionResult Create(CategoryDTO dto)
        {
            if (ModelState.IsValid)
            {
                var res = categoryService.Create(dto);
                if (res)
                {
                    TempData["Success"] = "Category created successfully!";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Error = "Failed to create category.";
            return View(dto);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToAction("Login", "Account");

            var data = categoryService.Get(id);
            if (data == null) return NotFound();
            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(CategoryDTO dto)
        {
            if (ModelState.IsValid)
            {
                var res = categoryService.Update(dto);
                if (res)
                {
                    TempData["Success"] = "Category updated successfully!";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Error = "Failed to update category.";
            return View(dto);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToAction("Login", "Account");

            var data = categoryService.Get(id);
            if (data == null) return NotFound();
            return View(data);
        }

        [HttpPost]
        public IActionResult Delete(int id, string Decision)
        {
            if (Decision.Equals("Yes"))
            {
                var res = categoryService.Delete(id);
                if (res)
                {
                    TempData["Success"] = "Category deleted successfully!";
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}