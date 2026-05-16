using BAL.DTOs;
using BAL.Services;
using Microsoft.AspNetCore.Mvc;


namespace APP.Controllers
{
    public class AccountController : Controller
    {
        UserService userService;

        public AccountController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginDTO());
        }

        [HttpPost]
        public IActionResult Login(LoginDTO dto)
        {
            var user = userService.Login(dto.Email, dto.Password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserName", user.Name);
                HttpContext.Session.SetString("UserRole", user.Role);
                HttpContext.Session.SetString("UserId", user.Id.ToString());

                if (user.Role.ToLower() == "admin")
                {
                    //TempData["Success"] = "Wellcome Admin";
                    TempData["Success"] = $"Welcome back, {user.Name}!";
                    return RedirectToAction("Index", "AdminDashboard");

                }

                else
                {
                    TempData["Success"] = $"Welcome back, {user.Name}!"; 
                    return RedirectToAction("Index", "CustomerDashboard");

                }
                    
               

            }
           

            ViewBag.Error = "Invalid email or password";
            return View(dto);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterDTO());
        }

        [HttpPost]
        public IActionResult Register(RegisterDTO dto)
        {
            if (dto.Password != dto.ConfirmPassword)
            {
                ViewBag.Error = "Passwords do not match";
                return View(dto);
            }

            if (userService.EmailExists(dto.Email))
            {
                ViewBag.Error = "Email already registered";
                return View(dto);
            }

            var result = userService.Register(dto);
            if (result)
            {
                TempData["Success"] = "Registered successfully! Please login.";
                return RedirectToAction("Login");
            }

            ViewBag.Error = "Registration failed";
            return View(dto);
        }
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}