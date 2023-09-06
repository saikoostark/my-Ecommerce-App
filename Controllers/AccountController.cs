using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using my_Ecommerce_App.Models;
using my_Ecommerce_App.Utils;
using my_Ecommerce_App.ViewModels;

namespace my_Ecommerce_App.Controllers
{
    public class AccountController : Controller
    {
        private readonly EcommerceDbContext dbContext;

        public AccountController(ILogger<AccountController> logger)
        {
            dbContext = new();
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.User.Identity!.IsAuthenticated)
                return LocalRedirect("/");

            RegisterUser user = new();
            return View(user);
        }


        [HttpPost]
        public IActionResult Register(RegisterUser reguser)
        {
            if (!ModelState.IsValid)
                return View(reguser);

            var salt = Hasher.GenerateSalt();
            var hashedPassword = Hasher.GenerateHash(reguser.Password!, salt);
            User user = new()
            {
                UserName = reguser.UserName,
                Email = reguser.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                Phone = reguser.Phone,
                Address = reguser.Address,
                Role = "RegularUser",

            };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            return LocalRedirect("/Account/Login");
        }


        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            if (HttpContext.User.Identity!.IsAuthenticated)
                return LocalRedirect("/");

            LoginUser user = new()
            {
                ReturnUrl = returnUrl
            };
            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginUser user)
        {
            ViewBag.InvalidCred = "username / Email does not match password";
            if (!ModelState.IsValid)
                return View(user);

            User? loggedUser = null;
            if (UserUtiles.IfEmail(user?.UserNameOrEmail))
            {
                loggedUser = dbContext.Users.FirstOrDefault(u => u.Email == (user!.UserNameOrEmail ?? ""));
                if (loggedUser == null)
                    return View(user);
            }
            else
            {
                loggedUser = dbContext.Users.FirstOrDefault(u => u.UserName == (user!.UserNameOrEmail ?? ""));
                if (loggedUser == null)
                    return View(user);
            }


            var salt = loggedUser.Salt;
            var hashedPassword = loggedUser.HashedPassword;
            var hashedPassword2 = Hasher.GenerateHash(user!.Password!, salt!);
            if (!hashedPassword!.Equals(hashedPassword2))
                return View(user);



            var claims = new List<Claim>(){
                new(ClaimTypes.NameIdentifier, loggedUser!.UserName!),
                new(ClaimTypes.Name, loggedUser!.UserName!),
                new(ClaimTypes.Email, loggedUser!.Email!),
                new(ClaimTypes.Role, loggedUser!.Role!),
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            principal, new AuthenticationProperties()
            {
                IsPersistent = user!.RememberMe,
            }
            );

            if (loggedUser!.Role!.Equals("AdminUser"))
            {
                return RedirectToAction("Index", "Home", "Admin");
            }

            return LocalRedirect(user?.ReturnUrl ?? "/");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }


        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }


    }
}