using Application.Community.Contract;
using Application.Community.Interfaces;
using Application.Events.Contract;
using Application.Events.Interfaces;
using FinderWebApp.Models.Request.Community;
using FinderWebApp.Models.ViewModels.Community;
using FinderWebApp.Models.ViewModels.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinderWebApp.Controllers
{
	public class CommunityController : Controller
	{
        private readonly ILogger<EventsController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ICommunityService _communityService;

        public CommunityController(ILogger<EventsController> logger, IWebHostEnvironment hostingEnvironment, ICommunityService communityService)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _communityService = communityService;
        }

        public IActionResult Index()
		{
			return View();
		}

        [HttpGet]
        [Authorize]
        public IActionResult Communities()
        {
            var communities = _communityService.GetAllCommunities().Result;

            if (communities.Count > 0)
            {
                var model = new List<CommunityViewModel>();

                foreach (var item in communities)
                {
                    var community = new CommunityViewModel
                    {
                        CommunityId = item.CommunityId,
                        CommunityName = item.CommunityName,
                        CommunityDescription = item.CommunityDescription,
                        CommunityImage = item.CommunityImage,
                        CreatorUserId = item.CreatorUserId,
                        CreatedDate = item.CreatedDate,
                        ModifierUserId = item.ModifierUserId,
                        ModifiedDate = item.ModifiedDate,
                        OldEventsIds = item.OldEventsIds
                    };
                    model.Add(community);
                }
                return View(model);
            }

            return View(new List<CommunityViewModel>());
        }

        [HttpGet]
		[Authorize]
		public IActionResult CreateCommunity()
		{
			return View();
		}


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCommunity(CreateCommunityRequest request)
        {
            string? userId = Request.Cookies["UserId"];
            var uniqueImgFileName = String.Empty;

            if (request.CommunityImage != null && request.CommunityImage.Length > 0)
            {
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "files", "CommunityImages");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                uniqueImgFileName = Guid.NewGuid().ToString() + "_" + request.CommunityImage.FileName;

                var filePath = Path.Combine(uploadsFolder, uniqueImgFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    request.CommunityImage.CopyTo(stream);
                }
            }

            var newcommunity = new CreateCommunityDto()
            {
                CommunityName = request.CommunityName,
                CommunityDescription = request.CommunityDescription,
                CommunityImage = uniqueImgFileName,
                CreatorUserId = Guid.Parse(userId),
            };

            var result = await _communityService.CreateCommunity(newcommunity);

            return Redirect("/CreateCommunity?id=" + result);
        }
    }
}
