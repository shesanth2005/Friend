using AutoMapper;
using Friend.API.Models.Requests;
using Friend.Application.Features.Accounts.Commands.LoginUser;
using Friend.Application.Features.Accounts.Commands.RegisterUser;
using Friend.Application.Features.Accounts.Dtos;

namespace Friend.API.Mappings
{
    public class ApiMappingProfile : Profile
    {
   
            public ApiMappingProfile()
            {
                // Map API request to DTO
                // This tells AutoMapper how to convert a RegisterRequest (from the API) into a RegisterDto.
                // It’s needed because the RegisterUserCommand contains a RegisterDto property, so AutoMapper must know how to populate it.
                CreateMap<RegisterRequest, RegisterDto>();


                // Map API request to Command and fill its RegisterDto property
                // This maps RegisterRequest directly to RegisterUserCommand. 
                // The ForMember part tells AutoMapper: "Take the RegisterRequest itself, map it into the RegisterDto property of the command."
                CreateMap<RegisterRequest, RegisterUserCommand>()
                        .ForMember(dest => dest.RegisterDto, opt => opt.MapFrom(src => src));

                CreateMap<LoginRequest, LoginDto>();

                CreateMap<LoginRequest, LoginUserCommand>()
                   .ForMember(dest => dest.LoginDto, opt => opt.MapFrom(src => src));
        }
        
    }
}
