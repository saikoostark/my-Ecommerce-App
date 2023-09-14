using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using my_Ecommerce_App.Models;

namespace my_Ecommerce_App.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly EcommerceDbContext DB;

        public CartController()
        {
            DB = new();
        }

        [HttpPost]
        public IActionResult AddToCart(int ProID)
        {
            ViewBag.cats = DB.Categorys.ToList();


            var prod = DB.Products.Find(ProID)!;
            var userid = ((ClaimsIdentity)User!.Identity!).Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).FirstOrDefault();

            int UserID = int.Parse(userid!);

            CartItem cart = DB.CartItems.FirstOrDefault(c => c.UserID == UserID && c.ProductID == ProID && c.OrderID == null);

            if (cart is null)
            {
                cart = new()
                {
                    Price = prod.Price,
                    Amount = 1,
                    ProductID = prod.ID,
                    UserID = UserID
                };
            }
            else
            {
                cart.Amount++;
            }

            DB.CartItems.Update(cart);
            DB.SaveChanges();

            return RedirectToAction("Index", "Categories");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            ViewBag.cats = DB.Categorys.ToList();


            var userid = ((ClaimsIdentity)User!.Identity!).Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).FirstOrDefault();

            int UserID = int.Parse(userid!);

            var cart = DB.CartItems.FirstOrDefault(c => c.UserID == UserID && c.ID == id);

            if (cart is not null)
            {
                DB.Remove(cart);
                DB.SaveChanges();
            }

            return RedirectToAction("Index", "Categories");
        }


        [HttpPost]
        public IActionResult Update(int proid, int itemid, int amount)
        {
            var userid = ((ClaimsIdentity)User!.Identity!).Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).FirstOrDefault();

            int UserID = int.Parse(userid!);

            var cart = DB.CartItems.FirstOrDefault(c => c.UserID == UserID && c.ID == itemid);
            cart!.Amount = amount;

            DB.Update(cart);
            DB.SaveChanges();

            return RedirectToAction("Index", "Categories");
        }

        public IActionResult ProccessOrder()
        {
            var userid = ((ClaimsIdentity)User!.Identity!).Claims
                   .Where(c => c.Type == ClaimTypes.NameIdentifier)
                   .Select(c => c.Value).FirstOrDefault();

            int UserID = int.Parse(userid!);

            var carts = DB.CartItems.Include(c => c.Product).Where(c => c.UserID == UserID && c.OrderID == null).ToList();

            decimal totalprice = 0;

            foreach (var cart in carts)
            {
                var pro = cart!.Product;
                if (cart.Amount > pro!.Quantity)
                    return RedirectToAction("Index", "Categories");

                totalprice += cart.Amount * cart.Price;
                pro.Quantity -= cart.Amount;
                DB.Update(pro);
            }

            var order = new Order()
            {
                OrderDate = new(),
                TotalPrice = totalprice,
                UserID = UserID,
                CartItems = carts
            };

            DB.Update(order);


            DB.SaveChanges();
            return RedirectToAction("Index", "Categories");

        }


    }
}