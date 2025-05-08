using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Api.Data;
using MovieStore.Api.Entities;
using MovieStore.Api.Models.Dtos;
using MovieStore.Api.Models.Requests;
using MovieStore.Api.Services.Interfaces;

namespace MovieStore.Api.Services.Implementations
{
    public class GenreService : IGenreService
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreService(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GenreDto>> GetAllAsync()
        {
            var genres = await _context.Genres.ToListAsync();
            return _mapper.Map<List<GenreDto>>(genres);
        }

        public async Task<GenreDto?> GetByIdAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            return genre == null ? null : _mapper.Map<GenreDto>(genre);
        }

        public async Task<GenreDto> CreateAsync(CreateGenreRequest request)
        {
            var genre = _mapper.Map<Genre>(request);
            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return _mapper.Map<GenreDto>(genre);
        }

        public async Task<bool> UpdateAsync(int id, UpdateGenreRequest request)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) return false;

            _mapper.Map(request, genre);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null) return false;

            genre.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
