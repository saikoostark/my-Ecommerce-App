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
    public IActionResult ViewAll()
    {



        return View(DB.Categorys.ToList());
    }


    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }



    [HttpPost]
    public IActionResult Add(Category cat, IFormFile ImgFile)
    {
        cat.Image = Image.IFormFileToByteArray(ImgFile);

        DB.Add(cat);
        DB.SaveChanges();

        return RedirectToAction("ViewAll");
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