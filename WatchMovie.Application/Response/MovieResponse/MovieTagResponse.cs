namespace WatchMovie.Application.Response.MovieResponse
{
    public class MovieTagResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid MovieId { get; set; }
        public virtual MovieResponse Movie { get; set; }
    }
}
