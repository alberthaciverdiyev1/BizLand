using Microsoft.AspNetCore.Mvc;

namespace BizLand.Areas.BizAdmin.Controllers
{
    [Area("BizAdmin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
