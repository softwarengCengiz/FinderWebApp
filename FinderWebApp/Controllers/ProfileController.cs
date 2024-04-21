using Microsoft.AspNetCore.Mvc;

namespace FinderWebApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IHttpContextAccessor HttpContextAccessor;

        public ProfileController(IHttpContextAccessor HttpContextAccessor)
        {
            this.HttpContextAccessor = HttpContextAccessor;
        }
        public IActionResult Index(Guid? id)
        {
            var role = HttpContextAccessor.HttpContext?.Request.Cookies["Role"];

            if (role == "0")
            {
                // burada öğrenci servise istek at
            } 
            return View();
        }
    }
}
