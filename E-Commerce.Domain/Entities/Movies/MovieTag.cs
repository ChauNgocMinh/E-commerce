namespace E_Commerce.Domain.Entities.Movies
{
    public class MovieTag
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public Guid MovieId { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
