using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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
        var allCats = DB.Categorys.ToList();
        var data = new HomeData() { Categories = allCats };
        return View(data);
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


