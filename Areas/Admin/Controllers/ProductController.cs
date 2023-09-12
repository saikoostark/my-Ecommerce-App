using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using my_Ecommerce_App.Models;
using my_Ecommerce_App.Utils;

namespace my_Ecommerce_App.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "AdminUser")]
public class ProductController : Controller
{
    private readonly EcommerceDbContext DB;

    public ProductController()
    {
        DB = new();
    }


    [HttpGet]
    public IActionResult ViewAll()
    {

        return View(DB.Products.ToList());
    }


    [HttpGet]
    public IActionResult Add()
    {

        ViewBag.Categories = DB.Categorys.ToList();
        return View();
    }



    [HttpPost]
    public IActionResult Add(Product prod, IFormFile ImgFile, List<int>? Categories)
    {
        prod.Image = Image.IFormFileToByteArray(ImgFile);

        prod.Categorys = new HashSet<Category>();

        foreach (var item in Categories!)
        {
            var found = DB.Categorys.Find(item);
            if (found != null)
            {
                prod.Categorys.Add(found);
            }
        }

        DB.Add(prod);
        DB.SaveChanges();

        return RedirectToAction("ViewAll");
    }


    [HttpPost]
    public IActionResult Delete(int ID)
    {
        var entityToDelete = DB.Products.Find(ID);
        if (entityToDelete != null)
        {
            DB.Products.Remove(entityToDelete);
            DB.SaveChanges();
        }
        return RedirectToAction("ViewAll");
    }




}