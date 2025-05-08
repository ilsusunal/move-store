using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieStore.Api.Data;
using MovieStore.Api.Entities;
using MovieStore.Api.Models.Dtos;
using MovieStore.Api.Models.Requests;
using MovieStore.Api.Services.Interfaces;

namespace MovieStore.Api.Services.Implementations
{
    public class ActorService : IActorService
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public ActorService(MovieStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ActorDto>> GetAllAsync()
        {
            var actors = await _context.Actors.ToListAsync();
            return _mapper.Map<List<ActorDto>>(actors);
        }

        public async Task<ActorDto?> GetByIdAsync(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            return actor == null ? null : _mapper.Map<ActorDto>(actor);
        }

        public async Task<ActorDto> CreateAsync(CreateActorRequest request)
        {
            var actor = _mapper.Map<Actor>(request);
            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();
            return _mapper.Map<ActorDto>(actor);
        }

        public async Task<bool> UpdateAsync(int id, UpdateActorRequest request)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor == null) return false;

            _mapper.Map(request, actor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var actor = await _context.Actors
                .Include(a => a.Movies)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (actor == null) return false;

            if (actor.Movies.Any()) return false;

            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
