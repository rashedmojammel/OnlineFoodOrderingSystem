using BAL.DTOs;
using BAL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace APP.Controllers
{
    public class CartController : Controller
    {
        FoodService foodService;
        OrderService orderService;

        public CartController(FoodService foodService, OrderService orderService)
        {
            this.foodService = foodService;
            this.orderService = orderService;
        }

   
        private List<CartItemDTO> GetCart()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(cartJson))
                return new List<CartItemDTO>();
            return JsonSerializer.Deserialize<List<CartItemDTO>>(cartJson)!;
        }

      
        private void SaveCart(List<CartItemDTO> cart)
        {
            HttpContext.Session.SetString("Cart",
                JsonSerializer.Serialize(cart));
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToAction("Login", "Account");

            var cart = GetCart();
            return View(cart);
        }

        public IActionResult Add(int foodId)
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToAction("Login", "Account");

            var food = foodService.Get(foodId);
            if (food == null)
                return RedirectToAction("Index", "Menu");

            var cart = GetCart();
            var existing = cart.FirstOrDefault(c => c.FoodId == foodId);

            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                cart.Add(new CartItemDTO
                {
                    FoodId = food.Id,
                    FoodName = food.Name,
                    Price = food.Price,
                    Quantity = 1
                });
            }

            SaveCart(cart);
            TempData["Success"] = $"{food.Name} added to cart!";
            return RedirectToAction("Index", "Menu");
        }

        public IActionResult Remove(int foodId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.FoodId == foodId);
            if (item != null)
                cart.Remove(item);
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        public IActionResult Increase(int foodId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.FoodId == foodId);
            if (item != null) item.Quantity++;
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        public IActionResult Decrease(int foodId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.FoodId == foodId);
            if (item != null)
            {
                item.Quantity--;
                if (item.Quantity <= 0)
                    cart.Remove(item);
            }
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Checkout()
        {
            if (HttpContext.Session.GetString("UserName") == null)
                return RedirectToAction("Login", "Account");

            var cart = GetCart();
            if (cart.Count == 0)
            {
                TempData["Error"] = "Your cart is empty!";
                return RedirectToAction("Index");
            }

            int userId = int.Parse(HttpContext.Session.GetString("UserId")!);
            var result = orderService.PlaceOrder(userId, cart);

            if (result)
            {
                HttpContext.Session.Remove("Cart");
                TempData["Success"] = "Order placed successfully!";
                return RedirectToAction("MyOrders", "Order");
            }

            TempData["Error"] = "Failed to place order. Try again.";
            return RedirectToAction("Index");
        }
    }
}