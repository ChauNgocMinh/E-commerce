using E_Commerce.Domain.Entities.Users;

namespace E_Commerce.Domain.Entities.Products
{
	public class NumberLikeProduct
	{
		public int Id { get; set; }
		public Guid ProductId { get; set; }
		public Guid UserId { set; get; }	
		public virtual Product Product { get;  set; }
		public virtual User User { get; set;}
	}
}
