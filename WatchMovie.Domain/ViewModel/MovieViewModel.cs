using Microsoft.AspNetCore.Http;

namespace WatchMovie.Domain.ViewModel
{
    public class MovieViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsFree { get; set; }
        public Guid CategoryId { get; set; }
        public string VideoFile { get; set; } 
        public IFormFile? MovieImgBanner { get; set; } 
    }
}
