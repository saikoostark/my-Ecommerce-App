using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using my_Ecommerce_App.Models;
using my_Ecommerce_App.Utils;

namespace my_Ecommerce_App.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "AdminUser")]
public class CategoryController : Controller
{
    private readonly EcommerceDbContext DB;

    public CategoryController()
    {
        DB = new();
    }


    [HttpGet]
    public IActionResult ViewAll(string? name)
    {
        List<Category>? cats = null;
        if (name != null)
            cats = DB.Categorys.Where(c => c.Name!.Contains(name)).ToList();
        else
            cats = DB.Categorys.ToList();
        return View(cats);
    }


    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }



    [HttpPost]
    public IActionResult Add(Category cat, IFormFile ImgFile, int IsOld)
    {
        if (IsOld == 1 && cat.ID != 0)
        {
            using EcommerceDbContext DB2 = new();
            cat.Image = DB2.Categorys.Find(cat.ID)!.Image;
        }
        else
        {
            cat.Image = Image.IFormFileToByteArray(ImgFile);
        }

        DB.Update(cat);
        DB.SaveChanges();

        return RedirectToAction("ViewAll");
    }


    [HttpGet]
    public IActionResult Edit(int ID)
    {
        var Editable = DB.Categorys.Find(ID);

        return View("Add", Editable);
    }


    [HttpPost]
    public IActionResult Delete(int ID)
    {
        var entityToDelete = DB.Categorys.Find(ID);
        if (entityToDelete != null)
        {
            DB.Categorys.Remove(entityToDelete);
            DB.SaveChanges();
        }
        return RedirectToAction("ViewAll");
    }


}