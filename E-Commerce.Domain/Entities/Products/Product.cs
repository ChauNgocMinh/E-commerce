using E_Commerce.Domain.Common;
namespace E_Commerce.Domain.Entities.Products
{
	public class Product : BaseEntity
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public decimal? BasePrice { get; set; }
		public string? Description { get; set; }
		public virtual ICollection<ProductImage> ProductImages { get; set; }
		public virtual ICollection<ProductTag> ProductTags { get; set; }
		public virtual ICollection<ProductFeedBack> ProductFeedBacks { get; set; }
	}
}
