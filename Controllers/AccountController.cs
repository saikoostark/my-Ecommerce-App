using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using my_Ecommerce_App.Models;
using my_Ecommerce_App.Utils;
using my_Ecommerce_App.ViewModels;
using my_Ecommerce_App.Areas.Admin.Controllers;
using Microsoft.EntityFrameworkCore;

namespace my_Ecommerce_App.Controllers
{
    public class AccountController : Controller
    {
        private readonly EcommerceDbContext DB;

        public AccountController(ILogger<AccountController> logger)
        {
            DB = new();
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.User.Identity!.IsAuthenticated)
                return LocalRedirect("/");


            ViewBag.cats = DB.Categorys.ToList();

            RegisterUser user = new();
            return View(user);
        }


        [HttpPost]
        public IActionResult Register(RegisterUser reguser)
        {
            ViewBag.cats = DB.Categorys.ToList();

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
            DB.Users.Add(user);
            DB.SaveChanges();

            return LocalRedirect("/Account/Login");
        }


        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            ViewBag.cats = DB.Categorys.ToList();


            if (HttpContext.User.Identity!.IsAuthenticated)
                return LocalRedirect("/");

            LoginUser user = new()
            {
                ReturnUrl = returnUrl
            };
            ViewBag.cats = DB.Categorys.ToList();

            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginUser user)
        {
            ViewBag.cats = DB.Categorys.ToList();


            ViewBag.InvalidCred = "username / Email does not match password";
            if (!ModelState.IsValid)
                return View(user);

            User? loggedUser = null;
            if (UserUtiles.IfEmail(user?.UserNameOrEmail))
            {
                loggedUser = DB.Users.FirstOrDefault(u => u.Email == (user!.UserNameOrEmail ?? ""));
                if (loggedUser == null)
                    return View(user);
            }
            else
            {
                loggedUser = DB.Users.FirstOrDefault(u => u.UserName == (user!.UserNameOrEmail ?? ""));
                if (loggedUser == null)
                    return View(user);
            }


            var salt = loggedUser.Salt;
            var hashedPassword = loggedUser.HashedPassword;
            var hashedPassword2 = Hasher.GenerateHash(user!.Password!, salt!);
            if (!hashedPassword!.Equals(hashedPassword2))
                return View(user);



            var claims = new List<Claim>(){
                new(ClaimTypes.NameIdentifier, loggedUser!.ID.ToString()),
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
                return RedirectToAction("Index", "Home", new { area = "Admin" });
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
            ViewBag.cats = DB.Categorys.ToList();


            if (User!.Identity!.IsAuthenticated)
            {
                var userid = ((ClaimsIdentity)User!.Identity!).Claims
                        .Where(c => c.Type == ClaimTypes.NameIdentifier)
                        .Select(c => c.Value).FirstOrDefault();

                int UserID = int.Parse(userid!);
                ViewBag.Carts = DB.CartItems.Include(c => c.Product).Where(c => c.UserID == UserID && c.OrderID == null);
            }

            return View();
        }


    }
}