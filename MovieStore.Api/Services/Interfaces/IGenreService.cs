using MovieStore.Api.Models.Dtos;
using MovieStore.Api.Models.Requests;

namespace MovieStore.Api.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetAllAsync();
        Task<GenreDto?> GetByIdAsync(int id);
        Task<GenreDto> CreateAsync(CreateGenreRequest request);
        Task<bool> UpdateAsync(int id, UpdateGenreRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
