using MovieStore.Api.Models.Dtos;
using MovieStore.Api.Models.Requests;

namespace MovieStore.Api.Services.Interfaces
{
    public interface IDirectorService
    {
        Task<IEnumerable<DirectorDto>> GetAllAsync();
        Task<DirectorDto?> GetByIdAsync(int id);
        Task<DirectorDto> CreateAsync(CreateDirectorRequest request);
        Task<bool> UpdateAsync(int id, UpdateDirectorRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
