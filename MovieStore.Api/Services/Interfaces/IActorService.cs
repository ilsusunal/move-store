using MovieStore.Api.Models.Dtos;
using MovieStore.Api.Models.Requests;

namespace MovieStore.Api.Services.Interfaces
{
    public interface IActorService
    {
        Task<IEnumerable<ActorDto>> GetAllAsync();
        Task<ActorDto?> GetByIdAsync(int id);
        Task<ActorDto> CreateAsync(CreateActorRequest request);
        Task<bool> UpdateAsync(int id, UpdateActorRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
