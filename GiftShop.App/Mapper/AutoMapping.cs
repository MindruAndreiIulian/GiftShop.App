using AutoMapper;
using GiftShop.App.Models.Account;
using GiftShop.App.Models.MediaModel;
using GiftShop.App.Models.Order;
using GiftShop.App.Models.OrderItems;
using GiftShop.App.Models.Product;
using GiftShop.App.Models.User;
using GiftShop.DataAccess.Entities;
using System;

namespace GiftShop.App.Mapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, RegisterVm>().ReverseMap()
                .ForMember(u => u.Password, s => s.MapFrom(r => r.Password))
                .ForMember(u => u.GenderId, g => g.MapFrom(r => Convert.ToInt32(r.GenderId)));
            CreateMap<Order, OrderVm>()
            .ForMember(o => o.Id, ex => ex.MapFrom(o => o.Id))
            .ForMember(o => o.userName, ex => ex.MapFrom(o => o.User.FirstName) )
            .ReverseMap();
            CreateMap<OrderItem, OrderItemsVm>().ReverseMap();
            CreateMap<Product, ProductVm>().ReverseMap();

            CreateMap<Media, MediaVm>().ReverseMap();
            CreateMap<User, UserVm>().ReverseMap();


        }
    }
}
