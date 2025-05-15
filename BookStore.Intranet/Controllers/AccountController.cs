using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using BookStore.Data.Data;
using BookStore.Data.Data.Entities;
using BookStore.Intranet.Models;
using Microsoft.AspNetCore.Authorization;

namespace BookStore.Intranet.Controllers
{
    public class AccountController : Controller
    {
        private readonly BookStoreContext _context;

        public AccountController(BookStoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.Users.FirstOrDefault(u => u.Username == model.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(model);
            }

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.Password, model.Password);
            if (result != PasswordVerificationResult.Success)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("UserId", user.IdUser.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Account");
        }
    }
}
