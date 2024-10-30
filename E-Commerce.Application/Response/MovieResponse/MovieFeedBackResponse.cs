namespace E_Commerce.Application.Response.MovieResponse
{
    public class MovieFeedBackResponse
    {
        public string? Comment { get; set; }
        public int Rate { set; get; }
        public Guid UserId { get; set; }
        public Guid MovieId { get; set; }
        public virtual MovieResponse Movie { get; set; }
        public virtual E_Commerce.Application.Response.UserResponse.UserResponse Users { get; set; }
    }
}
