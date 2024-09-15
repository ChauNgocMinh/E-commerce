namespace E_Commerce.Domain.Entities.Products
{
	public class ProductImage
	{
		public int Id { get; set; }	
		public string UrlImage { get; set; }
		public bool IsMain { get; set; }
		public Guid ProductId { get; set; }
		public virtual Product Product { get; set; }
	}
}
