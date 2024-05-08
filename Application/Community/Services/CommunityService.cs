using Application.Community.Contract;
using Application.Community.Interfaces;
using Application.Events.Contract;
using AutoMapper;
using Azure.Core;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Community.Services
{
    public class CommunityService : ICommunityService
    {
        private readonly AppDbContext context;

        public CommunityService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Guid> CreateCommunity(CreateCommunityDto request)
        {
            try
            {
                var newCommunity = new Domain.Community.Community
                {
                    CommunityId = Guid.NewGuid(),
                    CommunityName = request.CommunityName,
                    CommunityDescription = request.CommunityDescription,
                    CommunityImage = request.CommunityImage,
                    CreatorUserId = request.CreatorUserId,
                    CreatedDate = DateTime.Now
                };

                await context.Communities.AddAsync(newCommunity);

                await context.SaveChangesAsync();

                return newCommunity.CommunityId;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<CommunityDto>> GetAllCommunities()
        {
            var communities = context.Communities.ToList();

            var dtos = new List<CommunityDto>();
            foreach (var item in communities)
            {
                var dto = new CommunityDto
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

                dtos.Add(dto);
            }
            return dtos;
        }
    }
}
