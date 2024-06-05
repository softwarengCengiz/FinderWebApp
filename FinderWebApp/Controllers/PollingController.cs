using Application.Community.Interfaces;
using Application.Polling.Contract;
using Application.Polling.Interfaces;
using Domain.Community;
using FinderWebApp.Models.Request.Polling;
using FinderWebApp.Models.ViewModels.Community;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinderWebApp.Controllers
{
    public class PollingController : Controller
    {
        private readonly ICommunityService _communityService;
        private readonly IPollingService _pollingService;
        public PollingController(ICommunityService communityService, IPollingService pollingService)
        {
            _communityService = communityService;
            _pollingService = pollingService;
        }


        [HttpGet]
        [Authorize]
        public IActionResult StartPolling(Guid eventId)
        {
            var model = new CommunityViewModel();
            var UserId = Request.Cookies["UserId"];
            var ownCommunity = _communityService.GetCommunityByUserId(Guid.Parse(UserId)).Result;
            if (ownCommunity != null)
            {
                model.CommunityId = ownCommunity.CommunityId;
                model.CommunityName = ownCommunity.CommunityName;
                model.CommunityDescription = ownCommunity.CommunityDescription;
                model.CommunityImage = ownCommunity.CommunityImage;
                model.CreatorUserId = ownCommunity.CreatorUserId;
                model.CreatedDate = ownCommunity.CreatedDate;
                model.ModifierUserId = ownCommunity.ModifierUserId;
                model.ModifiedDate = ownCommunity.ModifiedDate;
                model.OldEventsIds = ownCommunity.OldEventsIds;
                model.EventId = eventId;
            }

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult StartPolling(StartPollingRequest request)
        {
            var UserId = Request.Cookies["UserId"];
            var pollingDto = new PollingDto
            {
                EventId = request.EventId,
                CommunityId = request.CommunityId,
                IsActive = true,
                CreatorUserId = Guid.Parse(UserId)
            };

            _pollingService.CreatePolling(pollingDto);

            return Redirect("/VoteEvent?eventId=" + request.EventId);
        }

        [HttpPost]
        [Authorize]
        public IActionResult VoteToEvent(VoteToEventRequest request)
        {
            var userId = Request.Cookies["UserId"];
            var voteToEventDto = new VoteToEventDto
            {
                EventId = request.EventId,
                CommunityId = request.CommunityId,
                UserId = Guid.Parse(userId)
            };

            var result = _pollingService.VoteToEvent(voteToEventDto).Result;
            if (result)
            {
                return Redirect("/VoteEvent?eventId=" + request.EventId);
            }

            return Redirect("/VoteEvent?eventId=" + request.EventId);
        }


        [Authorize]
        public IActionResult StartConversation()
        {
            return View();
        }
    }
}
