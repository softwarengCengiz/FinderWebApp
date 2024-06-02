using Application.Participant.Contract;
using Application.Student.Contract;

namespace Application.Participant.Interfaces
{
    public interface IParticipantService
    {
        Task<bool> AddParticipant(ParticipantDto request);
        Task<Guid> UpdateParticipant(ParticipantDto request);
        Task<ParticipantDto> GetParticipant(Guid userId);
    }
}
