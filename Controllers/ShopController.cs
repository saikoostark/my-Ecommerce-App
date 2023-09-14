using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using my_Ecommerce_App.Models;

namespace my_Ecommerce_App.Controllers
{

    public class ShopController : Controller
    {
        private readonly EcommerceDbContext DB;

        public ShopController()
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

            return View(DB.Products.ToList());
        }

    }
}