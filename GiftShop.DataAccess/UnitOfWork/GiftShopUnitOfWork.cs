using GiftShop.Common.Interfaces;
using GiftShop.DataAccess.Base;
using GiftShop.DataAccess.Context;
using GiftShop.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GiftShop.DataAccess.UnitOfWork
{
    public class GiftShopUnitOfWork : BaseUnitOfWork
    {
        public GiftShopUnitOfWork(GiftShopContext context) : base(context)
        {
        }

        private BaseRepository<User> user;

        public BaseRepository<User> User => user ?? (user = new BaseRepository<User>(context));


        private BaseRepository<Order> order;
        public BaseRepository<Order> Order => order ?? (order = new BaseRepository<Order>(context));

        private BaseRepository<Product> product;
        public BaseRepository<Product> Product => product ?? (product = new BaseRepository<Product>(context));

        private BaseRepository<Role> role;
        public BaseRepository<Role> Role => role ?? (role = new BaseRepository<Role>(context));

        private BaseRepository<OrderItem> orderItem;
        public BaseRepository<OrderItem> OrderItem => orderItem ?? (orderItem = new BaseRepository<OrderItem>(context));

        private BaseRepository<UsersRole> usersRole;
        public BaseRepository<UsersRole> UsersRole => usersRole ?? (usersRole = new BaseRepository<UsersRole>(context));

        private BaseRepository<FavoriteProduct> favProduct;
        public BaseRepository<FavoriteProduct> FavProduct => favProduct ?? (favProduct = new BaseRepository<FavoriteProduct>(context));

        private BaseRepository<Media> media;
        public BaseRepository<Media> Media => media ?? (media = new BaseRepository<Media>(context));



    }
}
