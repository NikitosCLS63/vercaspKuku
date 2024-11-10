using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1TEST.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Data;

namespace WebApplication1TEST.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private  AppDbContext _appDbContext;
        public HomeController( AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var userEmail = User.Identity.Name; // Получаем email пользователя
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user != null)
            {
                ViewBag.Greeting = $"Здравствуйте, {user.Name}. {user.Roliaya}";
            }
            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Catalog() {
            var products = await _appDbContext.Products.ToListAsync();
            return View(products);
        }
        public IActionResult Login()
        {
            if (HttpContext.Session.Keys.Contains("AutihatUsers")) { 
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost] 
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Login(LoginModel model) { 

            if (ModelState.IsValid)
            {
                Users user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(model.Email);
                    return RedirectToAction("Prof", new { email = model.Email });
                }
                ModelState.AddModelError("", "Некоректный логин и(или) пароль");
            }

            return View(model);
        }
        public ActionResult Register() { 
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Users person) {
            if (ModelState.IsValid)
            {
                var existingUser = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == person.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Пользователь с таким email уже существует.");
                    return View(person);
                }

                _appDbContext.Users.Add(person);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Login");
            }

            return View(person);
        }
        private async Task Authenticate(string name)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, name)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultNameClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> Logout() { 
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Remove("AuthUser");
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Prof(string email)
        {
            var users = await _appDbContext.Users.ToListAsync();

            ViewBag.Email = email;

            return View(users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}
