using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public IActionResult ViewAll(string? name)
    {

        List<Product>? prods = null;
        if (name != null)
            prods = DB.Products.Where(p => p.Name!.Contains(name)).ToList();
        else
            prods = DB.Products.ToList();
        return View(prods);
    }


    [HttpGet]
    public IActionResult Add()
    {

        ViewBag.Categories = DB.Categorys.ToList();
        return View();
    }



    [HttpPost]
    public IActionResult Add(Product prod, IFormFile ImgFile, List<int>? Categories, int IsOld)
    {
        if (IsOld == 1 && prod.ID != 0)
        {
            using EcommerceDbContext DB2 = new();
            prod.Image = DB2.Products.Find(prod.ID)!.Image;
        }
        else
        {
            prod.Image = Image.IFormFileToByteArray(ImgFile);
        }

        if (prod.ID == 0)
        {
            prod.Categorys = new HashSet<Category>();

            foreach (var item in Categories!)
            {
                var found = DB.Categorys.Find(item);
                if (found != null)
                {
                    prod.Categorys.Add(found);
                }
            }
        }




        DB.Update(prod);
        DB.SaveChanges();

        return RedirectToAction("ViewAll");
    }

    [HttpGet]
    public IActionResult Edit(int ID)
    {
        var Editable = DB.Products.Include(p => p.Categorys).FirstOrDefault(p => p.ID == ID);
        ViewBag.Categories = DB.Categorys.ToList();
        return View("Add", Editable);
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