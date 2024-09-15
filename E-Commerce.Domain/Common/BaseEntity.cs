namespace E_Commerce.Domain.Common
{
	public class BaseEntity
	{
		public Guid Id { get; set; } = Guid.NewGuid();
		public Guid CreatedBy { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public Guid UpdatedBy { get; set; }
		public DateTime UpdatedAt { get; set; }
	}
}
