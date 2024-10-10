using AutoMapper;
using E_Commerce.Application.Response.ProductResponse;
using E_Commerce.Domain.Entities.Products;
namespace E_Commerce.Application.AutoMapping.ToProductMapping
{
    public class ToProductMapping : Profile
    {
        public ToProductMapping() 
        {
            CreateMap<Product, ProductResponse>();
        }
    }
}
