using Microsoft.AspNetCore.Mvc;

namespace SurveyHos.Theme.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
