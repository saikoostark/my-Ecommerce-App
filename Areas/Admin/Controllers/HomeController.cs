using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace my_Ecommerce_App.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "AdminUser")]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }


    }
}