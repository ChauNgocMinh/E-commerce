namespace E_Commerce.Application.Response.ProductResponse
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal? BasePrice { get; set; }
        public string? Description { get; set; }
        public string MainImageUrl => ProductImages?.FirstOrDefault(img => img.IsMain)?.UrlImage ?? "";
        public virtual ICollection<ProductImageResponse> ProductImages { get; set; }
    }
}
