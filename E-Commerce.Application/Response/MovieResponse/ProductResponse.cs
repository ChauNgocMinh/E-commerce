namespace E_Commerce.Application.Response.MovieResponse
{
    public class MovieResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal? BasePrice { get; set; }
        public string? Description { get; set; }
        public string MainImageUrl => MovieImages?.FirstOrDefault(img => img.IsMain)?.UrlImage ?? "";
        public virtual ICollection<MovieImageResponse> MovieImages { get; set; }
    }
}
