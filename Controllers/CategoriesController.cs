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

            return View(DB.Categorys.ToList());
        }


        public IActionResult Category(int ID)
        {

            var prods = DB.Categorys.Include(cat => cat.Products).FirstOrDefault(cat => cat.ID == ID)?.Products?.ToList();
            if (prods is null)
                return RedirectToAction("Index");

            return View(prods);
        }





    }
}