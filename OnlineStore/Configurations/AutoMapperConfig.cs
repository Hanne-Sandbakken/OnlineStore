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
            //mapper Product-objekter til ProductDto-objekter og motsatt
            CreateMap<Product, GetProductDto>().ReverseMap();
            CreateMap<Product, PostToCartDto>().ReverseMap();

            //mapper Cart-objekter til CartDto-objekter og motsatt
            CreateMap<Cart, GetCartDto>().ReverseMap();
            CreateMap<Cart, PostProductToCartDto>().ReverseMap();
            CreateMap<Cart, PostCartToOrderDto>().ReverseMap();
            //CreateMap<Cart, PostToCartDto>().ReverseMap();

            //mapper Order-objekter til OrderDto-objekter og motsatt:
            CreateMap<Order, PostOrderDto>().ReverseMap();
        }
    }
}
