using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using my_Ecommerce_App.Models;
using my_Ecommerce_App.ViewModels;

namespace my_Ecommerce_App.Controllers;

public class HomeController : Controller
{
    readonly EcommerceDbContext DB;

    public HomeController()
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

        ViewBag.pros = DB.Products.ToList();

        var allCats = DB.Categorys.ToList();
        var data = new HomeData() { Categories = allCats };
        return View(data);
    }

}


