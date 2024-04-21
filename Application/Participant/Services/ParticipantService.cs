using Application.Participant.Contract;
using Application.Participant.Interfaces;
using Application.Student.Contract;
using Infrastructure.Data;

namespace Application.Participant.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly AppDbContext context;

        public ParticipantService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<bool> AddParticipant(ParticipantDto request)
        {
            try
            {
                var participant = new Domain.Participant.Participant
                {
                    Id = new Guid(),
                    UserId = request.UserId,
                    Job = request.Job,
                    Company = request.Company,
                    CV = request.CV,
                    CreatedDate = System.DateTime.Now,
                };

                await context.Participants.AddAsync(participant);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
