using FinderWebApp.Models.Request.Community;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinderWebApp.Controllers
{
	public class CommunityController : Controller
	{

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		[Authorize]
		public IActionResult CreateCommunity()
		{
			return View();
		}


        [HttpPost]
        [Authorize]
        public IActionResult CreateCommunity(CreateCommunityRequest request)
        {
            return View();
        }
    }
}
