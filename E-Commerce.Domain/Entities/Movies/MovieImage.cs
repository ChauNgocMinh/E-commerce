using E_Commerce.Domain.Entities.Movies;

namespace E_Commerce.Domain.Entities.Movies
{
	public class MovieImage
	{
		public int Id { get; set; }	
		public string UrlImage { get; set; }
		public bool IsMain { get; set; }
		public Guid MovieId { get; set; }
		public virtual Movie Movie { get; set; }
	}
}
