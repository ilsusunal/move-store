using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Api.Data;
using MovieStore.Api.Entities;
using MovieStore.Api.Models.Dtos;
using MovieStore.Api.Models.Requests;
using MovieStore.Api.Services.Interfaces;

namespace MovieStore.Api.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public MovieService(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MovieDto>> GetAllAsync()
        {
            var movies = await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.Director)
                .ToListAsync();

            return _mapper.Map<List<MovieDto>>(movies);
        }

        public async Task<MovieDto?> GetByIdAsync(int id)
        {
            var movie = await _context.Movies
                .Include(m => m.Genre)
                .Include(m => m.Director)
                .FirstOrDefaultAsync(m => m.Id == id);

            return movie == null ? null : _mapper.Map<MovieDto>(movie);
        }

        public async Task<MovieDto> CreateAsync(CreateMovieRequest request)
        {
            var movie = _mapper.Map<Movie>(request);

            // actor ilişkisini elle kurmamız gerekiyor
            movie.Actors = await _context.Actors
                .Where(a => request.ActorIds.Contains(a.Id))
                .ToListAsync();

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<bool> UpdateAsync(int id, UpdateMovieRequest request)
        {
            var movie = await _context.Movies
                .Include(m => m.Actors)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null) return false;

            _mapper.Map(request, movie);

            // actor ilişkisini güncelle
            movie.Actors = await _context.Actors
                .Where(a => request.ActorIds.Contains(a.Id))
                .ToListAsync();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return false;

            movie.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
