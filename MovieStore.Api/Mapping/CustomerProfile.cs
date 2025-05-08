using AutoMapper;
using MovieStore.Api.Entities;
using MovieStore.Api.Models.Dtos;
using MovieStore.Api.Models.Requests;

namespace MovieStore.Api.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));

            CreateMap<CreateCustomerRequest, Customer>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<UpdateCustomerRequest, Customer>();
        }
    }
}
