using AutoMapper;
using E_Commerce.Application.Response.UserResponse;
using E_Commerce.Domain.Entities.Users;

namespace E_Commerce.Application.AutoMapping.ToUserMapping
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
