namespace E_Commerce.Domain.Entities.Products
{
    public class ProductTag
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
