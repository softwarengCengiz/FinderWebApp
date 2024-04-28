using Application.Events.Contract;
using Application.Events.Interfaces;
using FinderWebApp.Models;
using FinderWebApp.Models.Request.Events;
using FinderWebApp.Models.ViewModels.Events;
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

		public EventsController(ILogger<EventsController> logger, IWebHostEnvironment hostingEnvironment, IEventService eventService)
		{
			_logger = logger;
			_hostingEnvironment = hostingEnvironment;
			_eventService = eventService;
		}

		public IActionResult Index()
		{
			return View();
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
				EventImage = uniqueImgFileName,
				IsActive = request.IsActive,
				UserId = Guid.Parse(userId)
			};

			var result = await _eventService.CreateEvent(newEvent);

			return Redirect("/CreateEvent?id=" + result);
		}


		//[HttpGet]
		//[Authorize]
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
					Polling = item.Polling,
					IsActive = item.IsActive,
					CreatedDate	= item.CreatedDate,
					ModifiedDate = item.ModifiedDate,
				};
				model.Add(myEvent);
			}
			return View(model);
		}

	}
}
