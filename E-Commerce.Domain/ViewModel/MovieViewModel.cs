using Microsoft.AspNetCore.Http;

namespace E_Commerce.Domain.ViewModel
{
    public class MovieViewModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsFree { get; set; }
        public Guid CategoryId { get; set; }
        public IFormFile? VideoFile { get; set; } 
        public IFormFile? MovieImgBanner { get; set; } 
    }
}
