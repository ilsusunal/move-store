using AutoMapper;
using MovieStore.Api.Entities;
using MovieStore.Api.Models.Dtos;
using MovieStore.Api.Models.Requests;

namespace MovieStore.Api.Mapping
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(dest => dest.DirectorName, opt => opt.MapFrom(src => src.Director.FirstName + " " + src.Director.LastName));

            CreateMap<CreateMovieRequest, Movie>();
            CreateMap<UpdateMovieRequest, Movie>();
        }
    }
}
