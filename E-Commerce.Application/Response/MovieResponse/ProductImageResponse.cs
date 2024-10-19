namespace E_Commerce.Application.Response.MovieResponse
{
    public class MovieImageResponse
    {
        public int Id { get; set; }
        public string UrlImage { get; set; }
        public int Thunderbool { get; set; }
        public bool IsMain { get; set; }
        public Guid MovieId { get; set; }
    }
}
