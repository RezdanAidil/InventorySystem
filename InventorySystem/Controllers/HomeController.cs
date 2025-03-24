using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult About() => View();
        public IActionResult FAQ() => View();
    }
}
