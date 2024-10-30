using WatchMovie.Domain.Common;

namespace WatchMovie.Domain.Entities.Movies
{
    public class MovieCategory :  BaseEntity
    {
        public string Title {  get; set; }
        public string? Description { get; set; } 
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
