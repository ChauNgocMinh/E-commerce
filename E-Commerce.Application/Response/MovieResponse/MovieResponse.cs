namespace E_Commerce.Application.Response.MovieResponse
{
    public class MovieResponse
    {
        public Guid Id { get; set; }
        public string UrlMedia { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsFree { get; set; }
        public Guid CategoryId { get; set; }
        public string MainImageUrl => MovieImages?.FirstOrDefault(img => img.IsMain)?.UrlImage ?? "";
        public virtual ICollection<MovieImageResponse> MovieImages { get; set; }
        public virtual ICollection<MovieTagResponse> MovieTags { get; set; }
    }
}
