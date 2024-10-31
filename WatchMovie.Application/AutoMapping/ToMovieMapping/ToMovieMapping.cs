using AutoMapper;
using WatchMovie.Application.Response.MovieResponse;
using WatchMovie.Domain.Entities.Movies;
using WatchMovie.Domain.ViewModel;
namespace WatchMovie.Application.AutoMapping.ToMovieMapping
{
    public class ToMovieMapping : Profile
    {
        public ToMovieMapping() 
        {
            CreateMap<Movie, MovieResponse>();
            CreateMap<MovieFeedBack, MovieFeedBackResponse>();
            CreateMap<MovieImage, MovieImageResponse>();
            CreateMap<MovieTag, MovieTagResponse>();
            CreateMap<MovieViewModel, Movie>().ForMember(dest => dest.UrlMedia, opt => opt.Ignore());
            CreateMap<Movie, MovieViewModel>();
        }
    }
}
