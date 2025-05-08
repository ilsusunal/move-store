using MovieStore.Api.Models.Dtos;
using MovieStore.Api.Models.Requests;

namespace MovieStore.Api.Services.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieDto>> GetAllAsync();
        Task<MovieDto?> GetByIdAsync(int id);
        Task<MovieDto> CreateAsync(CreateMovieRequest request);
        Task<bool> UpdateAsync(int id, UpdateMovieRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
