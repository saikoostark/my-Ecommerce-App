using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace my_Ecommerce_App.Areas.Admin.Controllers;



[Area("Admin")]
[Authorize(Roles = "AdminUser")]
public class ProductController : Controller
{

    public IActionResult Index()
    {
        return View();
    }


    public IActionResult Add()
    {
        return View();
    }
}