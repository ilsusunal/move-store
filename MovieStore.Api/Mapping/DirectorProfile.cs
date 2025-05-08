using AutoMapper;
using MovieStore.Api.Entities;
using MovieStore.Api.Models.Dtos;
using MovieStore.Api.Models.Requests;

namespace MovieStore.Api.Mapping
{
    public class DirectorProfile : Profile
    {
        public DirectorProfile()
        {
            CreateMap<Director, DirectorDto>()
                .ForMember(dest => dest.FullName,
                           opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));

            CreateMap<CreateDirectorRequest, Director>();
            CreateMap<UpdateDirectorRequest, Director>();
        }
    }
}
