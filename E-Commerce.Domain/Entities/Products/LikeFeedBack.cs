using E_Commerce.Domain.Entities.Users;

namespace E_Commerce.Domain.Entities.Products
{
	public class LikeFeedBack
	{
		public int Id { get; set; }	
		public Guid ProductFeedBackId { get; set; }
		public Guid UserId { get; set; }
		public virtual ProductFeedBack ProductFeedBack { get; set; }
		public virtual User User { get; set; }
	}
}
