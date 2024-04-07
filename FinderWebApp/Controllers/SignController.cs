using Microsoft.AspNetCore.Mvc;

namespace FinderWebApp.Controllers
{
    public class SignController : Controller
    {
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
