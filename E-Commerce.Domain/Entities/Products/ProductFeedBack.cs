using E_Commerce.Domain.Common;
using E_Commerce.Domain.Entities.Users;
using System.ComponentModel;
namespace E_Commerce.Domain.Entities.Products
{
	public class ProductFeedBack : BaseEntity
	{
		public string Description { get; set; }
		public int Rate { set; get; }
		public Guid ProductId { get; set; }
		public virtual Product Product { get; set; }
	}
}
