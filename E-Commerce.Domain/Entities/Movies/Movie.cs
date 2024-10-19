using E_Commerce.Domain.Common;
namespace E_Commerce.Domain.Entities.Movies
{
	public class Movie : BaseEntity
	{
		public string UrlMedia { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public bool IsFree { get; set; }
		public virtual ICollection<MovieImage> MovieImages { get; set; }
		public virtual ICollection<MovieTag> MovieTags { get; set; }
		public virtual ICollection<MovieFeedBack> MovieFeedBacks { get; set; }
	}
}
