using Application.AI.Interfaces;
using FinderWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinderWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAIService _aiService;

        public HomeController(ILogger<HomeController> logger, IAIService aiService)
        {
            _logger = logger;
            _aiService = aiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetChatResponse(string userInput)
        {
            var response = await _aiService.GetResponseAsync(userInput);
            return Json(new { response });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public class UserInput
        {
            public string Input { get; set; }
        }
    }
}
