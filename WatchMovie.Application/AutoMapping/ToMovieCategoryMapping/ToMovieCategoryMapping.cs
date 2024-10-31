using AutoMapper;
using WatchMovie.Application.Response.MovieResponse;
using WatchMovie.Domain.Entities.Movies;
namespace WatchMovie.Application.AutoMapping.ToMovieCategoryMapping
{
    public class ToMovieCategoryMapping : Profile
    {
        public ToMovieCategoryMapping() {
            CreateMap<MovieCategory, MovieCategoryResponse>();
        }
    }
}
