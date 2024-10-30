using AutoMapper;
using WatchMovie.Application.Response.UserResponse;
using WatchMovie.Domain.Entities.Users;

namespace WatchMovie.Application.AutoMapping.ToUserMapping
{
    public class ToUserMapping : Profile
    {
        public ToUserMapping() 
        {
            CreateMap<User, UserResponse>()
            .ForMember(dest => dest.Roles, opt => opt.Ignore()); ;
        }
    }
}
