using AutoMapper;
using E_Commerce.Application.Response.MovieCategoryResponse;
using E_Commerce.Domain.Entities.Movies;
namespace E_Commerce.Application.AutoMapping.ToMovieCategoryMapping
{
    public class ToMovieCategoryMapping : Profile
    {
        public ToMovieCategoryMapping() {
            CreateMap<MovieCategory, MovieCategoryResponse>();
        }
    }
}
