using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using BookStore.Data.Data;
using BookStore.Data.Data.Entities;
using BookStore.PortalWWW.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BookStore.PortalWWW.Controllers
{
    public class AccountController : Controller
    {
        private readonly BookStoreContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public AccountController(BookStoreContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var username = User.Identity?.Name;
            if (username == null)
                return RedirectToAction("Login", "Account");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
                return RedirectToAction("Login", "Account");

            var model = new UserProfileViewModel
            {
                IdUser = user.IdUser,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Street = user.Street,
                City = user.City,
                PostalCode = user.PostalCode,
                Orders = await _context.Orders
                    .Where(o => o.IdUser == user.IdUser)
                    .OrderByDescending(o => o.OrderDate)
                    .Select(o => new OrderViewModel
                    {
                        OrderId = o.IdOrder,
                        DatePlaced = o.OrderDate,
                        Status = o.OrderStatus.Name
                    })
                    .ToListAsync(),
                Reviews = await _context.Reviews
                    .Where(r => r.IdUser == user.IdUser)
                    .OrderByDescending(r => r.DateAdded)
                    .Select(r => new EditReviewViewModel
                    {
                        IdReview = r.IdReview,
                        BookId = r.IdBook,
                        BookTitle = r.Book.Title,
                        Rating = r.Rating,
                        Comment = r.Comment,
                        DateAdded = r.DateAdded
                    })
                    .ToListAsync()
            };

            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
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

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);
            if (result != PasswordVerificationResult.Success)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                new Claim(ClaimTypes.Role, user.Role ?? "Customer")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_context.Users.Any(u => u.Username == model.Username))
            {
                ModelState.AddModelError(nameof(model.Username), "Username already exists.");
                return View(model);
            }

            var user = new User
            {
                Username = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Street = model.Street,
                City = model.City,
                PostalCode = model.PostalCode,
                Role = "Customer",
                Password = _passwordHasher.HashPassword(null, model.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Edit()
        {
            if (!User.Identity?.IsAuthenticated ?? true)
                return RedirectToAction("Login");

            var username = User.Identity.Name;
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
                return NotFound();

            var model = new UserProfileViewModel
            {
                IdUser = user.IdUser,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Street = user.Street,
                City = user.City,
                PostalCode = user.PostalCode,
                Password = null
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserProfileViewModel model)
        {
            if (!User.Identity?.IsAuthenticated ?? true)
                return RedirectToAction("Login");

            if (!ModelState.IsValid)
                return View(model);

            var user = _context.Users.FirstOrDefault(u => u.IdUser == model.IdUser);

            if (user == null)
                return NotFound();

            if (!string.Equals(user.Username, model.Username, StringComparison.OrdinalIgnoreCase))
            {
                bool usernameExists = _context.Users.Any(u => u.Username == model.Username && u.IdUser != model.IdUser);
                if (usernameExists)
                {
                    ModelState.AddModelError(nameof(model.Username), "Username already exists.");
                    return View(model);
                }
            }

            user.Username = model.Username;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Street = model.Street;
            user.City = model.City;
            user.PostalCode = model.PostalCode;

            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                user.Password = _passwordHasher.HashPassword(user, model.Password);
            }

            await _context.SaveChangesAsync();

            if (!string.Equals(User.Identity.Name, model.Username, StringComparison.OrdinalIgnoreCase))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            }

            TempData["SuccessMessage"] = "Account details updated.";
            return RedirectToAction("Index");
        }
    }
}
