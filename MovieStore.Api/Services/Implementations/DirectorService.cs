using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Api.Data;
using MovieStore.Api.Entities;
using MovieStore.Api.Models.Dtos;
using MovieStore.Api.Models.Requests;
using MovieStore.Api.Services.Interfaces;

namespace MovieStore.Api.Services.Implementations
{
    public class DirectorService : IDirectorService
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public DirectorService(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DirectorDto>> GetAllAsync()
        {
            var directors = await _context.Directors.ToListAsync();
            return _mapper.Map<List<DirectorDto>>(directors);
        }

        public async Task<DirectorDto?> GetByIdAsync(int id)
        {
            var director = await _context.Directors.FindAsync(id);
            return director == null ? null : _mapper.Map<DirectorDto>(director);
        }

        public async Task<DirectorDto> CreateAsync(CreateDirectorRequest request)
        {
            var director = _mapper.Map<Director>(request);
            _context.Directors.Add(director);
            await _context.SaveChangesAsync();
            return _mapper.Map<DirectorDto>(director);
        }

        public async Task<bool> UpdateAsync(int id, UpdateDirectorRequest request)
        {
            var director = await _context.Directors.FindAsync(id);
            if (director == null) return false;

            _mapper.Map(request, director);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var hasMovies = await _context.Movies.AnyAsync(m => m.DirectorId == id);
            if (hasMovies) return false;

            var director = await _context.Directors.FindAsync(id);
            if (director == null) return false;

            _context.Directors.Remove(director);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
