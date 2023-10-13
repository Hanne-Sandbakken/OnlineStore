using AutoMapper;
using OnlineStore.Data;
using OnlineStore.Dto.Cart;
using OnlineStore.Dto.Order;
using OnlineStore.Dto.Product;

namespace OnlineStore.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //map Product-objects to dtos, and reversed.
            CreateMap<Product, GetProductDto>().ReverseMap();
            CreateMap<Product, PostToCartDto>().ReverseMap();

            //map Cart-objects to dtos, and reversed.
            CreateMap<Cart, GetCartDto>().ReverseMap();
            CreateMap<Cart, PostProductToCartDto>().ReverseMap();
            CreateMap<Cart, PostCartToOrderDto>().ReverseMap();
        }
    }
}
