using Application.Community.Interfaces;
using Application.Events.Contract;
using Application.Events.Interfaces;
using Application.Polling.Interfaces;
using Domain.Polling;
using FinderWebApp.Models;
using FinderWebApp.Models.Request.Events;
using FinderWebApp.Models.ViewModels.Community;
using FinderWebApp.Models.ViewModels.Events;
using FinderWebApp.Models.ViewModels.Polling;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinderWebApp.Controllers
{
    public class EventsController : Controller
    {
        private readonly ILogger<EventsController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IEventService _eventService;
        private readonly IPollingService _pollingService;
        private readonly ICommunityService _communityService;

        public EventsController(ILogger<EventsController> logger,
            IWebHostEnvironment hostingEnvironment,
            IEventService eventService,
            IPollingService pollingService,
            ICommunityService communityService)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _eventService = eventService;
            _pollingService = pollingService;
            _communityService = communityService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var events = _eventService.GetAllEvents().Result;
            if (events.Count > 0)
            {
                var model = new List<MyEventsViewModel>();

                foreach (var item in events)
                {
                    var myEvent = new MyEventsViewModel
                    {
                        EventId = item.EventId,
                        UserId = Guid.Parse(item.UserId.ToString()),
                        EventHeader = item.EventHeader,
                        EventDetail = item.EventDetail,
                        MinimumQuantity = item.MinimumQuantity,
                        EventImage = item.EventImage,
                        Polling = item.Polling,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        ModifiedDate = item.ModifiedDate,
                    };
                    model.Add(myEvent);
                }
                return View(model);
            }

            return View(new List<EventsDto>());
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateEvent()
        {
            return View();
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateEvent(CreateEventRequest request)
        {
            string? userId = Request.Cookies["UserId"];
            var uniqueImgFileName = String.Empty;

            if (request.EventImage != null && request.EventImage.Length > 0)
            {
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "files", "EventsImages");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                uniqueImgFileName = Guid.NewGuid().ToString() + "_" + request.EventImage.FileName;

                var filePath = Path.Combine(uploadsFolder, uniqueImgFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    request.EventImage.CopyTo(stream);
                }
            }

            var newEvent = new CreateEventRequestDto()
            {
                EventHeader = request.EventHeader,
                EventDetail = request.EventDetail,
                MinimumQuantity = request.MinimumQuantity,
                EventImage = uniqueImgFileName,
                IsActive = request.IsActive,
                UserId = Guid.Parse(userId)
            };

            var result = await _eventService.CreateEvent(newEvent);

            return Redirect("/CreateEvent?id=" + result);
        }


        [HttpGet]
        [Authorize]
        public IActionResult MyEvents(Guid id)
        {
            var myEvents = _eventService.GetMyEvents(id).Result;

            var model = new List<MyEventsViewModel>();

            foreach (var item in myEvents)
            {
                var myEvent = new MyEventsViewModel
                {
                    EventId = item.EventId,
                    UserId = Guid.Parse(item.UserId.ToString()),
                    EventHeader = item.EventHeader,
                    EventDetail = item.EventDetail,
                    EventImage = item.EventImage,
                    MinimumQuantity = item.MinimumQuantity,
                    Polling = item.Polling,
                    IsActive = item.IsActive,
                    CreatedDate = item.CreatedDate,
                    ModifiedDate = item.ModifiedDate,
                };
                model.Add(myEvent);
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult ShowEvent(Guid id)
        {
            var currentEvent = _eventService.ShowEvent(id).Result;
            var model = new MyEventsViewModel
            {
                EventId = currentEvent.EventId,
                UserId = Guid.Parse(currentEvent.UserId.ToString()),
                EventHeader = currentEvent.EventHeader,
                EventDetail = currentEvent.EventDetail,
                EventImage = currentEvent.EventImage,
                MinimumQuantity = currentEvent.MinimumQuantity,
                Polling = currentEvent.Polling,
                IsActive = currentEvent.IsActive,
                CreatedDate = currentEvent.CreatedDate,
                ModifiedDate = currentEvent.ModifiedDate,
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult VoteEvent(Guid eventId)
        {
            var model = new VoteEventViewModel();
            model.Pollings = new List<PollingViewModel>();
            model.Communities = new List<CommunityViewModel>();
            var UserId = Request.Cookies["UserId"]; //Giriþ yapan kullanýcý
            var currentEvent = _eventService.ShowEvent(eventId).Result; // Etkinlik
            var currentPollings = _pollingService.GetPollingsByEvent(eventId).Result; //Etkinlik Oylarý
            var votedCommunities = _communityService.GetVotedCommunities(currentPollings.Select(x => x.CommunityId).ToList()).Result; //Oylarýn Topluluklarý

            if (!string.IsNullOrEmpty(UserId))
            {
                var IsCommunityOwner = _communityService.GetCommunityByUserId(Guid.Parse(UserId)); //Giriþ yapan kullanýcý herhangi bir topluluk sahibi mi
                if (IsCommunityOwner.Result != null)
                {
                    model.IsCommunityOwner = true;
                }
            }

            if (currentEvent != null)
            {
                model.Event = new MyEventsViewModel
                {
                    EventId = currentEvent.EventId,
                    UserId = Guid.Parse(currentEvent.UserId.ToString()),
                    EventHeader = currentEvent.EventHeader,
                    EventDetail = currentEvent.EventDetail,
                    EventImage = currentEvent.EventImage,
                    MinimumQuantity = currentEvent.MinimumQuantity,
                    Polling = currentEvent.Polling,
                    IsActive = currentEvent.IsActive,
                    CreatedDate = currentEvent.CreatedDate,
                    ModifiedDate = currentEvent.ModifiedDate,
                };
            }

            if (currentPollings.Any())
            {
                foreach (var item in currentPollings)
                {
                    var polling = new PollingViewModel
                    {
                        PollingId = item.PollingId,
                        EventId = item.EventId,
                        CommunityId = item.CommunityId,
                        Quantity = item.Quantity,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        CreatorUserId = item.CreatorUserId,
                        ModifiedDate = item.ModifiedDate,
                        ModifierUserId = item.ModifierUserId
                    };
                    model.Pollings.Add(polling);
                }
            }

            if (votedCommunities.Any())
            {
                foreach (var item in votedCommunities)
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
                    model.Communities.Add(community);
                }
            }

            return View(model);
        }

    }
}