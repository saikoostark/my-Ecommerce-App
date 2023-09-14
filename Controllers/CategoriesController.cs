using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using my_Ecommerce_App.Models;

namespace my_Ecommerce_App.Controllers
{

    public class CategoriesController : Controller
    {
        private readonly EcommerceDbContext DB;

        public CategoriesController()
        {
            DB = new();
        }


        public IActionResult Index()
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

            return View(DB.Categorys.ToList());
        }

        public IActionResult Category(int ID, string name)
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


            List<Product>? prods;
            if (ID == 0)
                prods = DB.Products.Include(p => p.Categorys).Where(p => p!.Name!.Contains(name ?? "")).ToList();
            else
                prods = DB.Categorys.Include(p => p.Products).FirstOrDefault(c => c.ID == ID)?
                .Products!.Where(p => p!.Name!.Contains(name ?? "")).ToList();


            if (name is null || name.Length == 0)
                ViewData["Title"] = DB.Categorys.Find(ID)?.Name;
            else
                ViewData["Title"] = "Products";

            return View(prods);
        }


        public IActionResult Products(int cat, string? name)
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

            List<Product> prods;
            var pre = DB.Products.Include(p => p.Categorys);
            if (cat == 0)
                prods = pre.Where(p => p.Name.Contains(name)).ToList();
            else
                prods = DB.Categorys.Include(p => p.Products).FirstOrDefault(c => c.ID == cat)?.Products
                .Where(p => p.Name.Contains(name)).ToList();

            ViewData["Title"] = "Products";

            return View(prods);
        }


    }
}