using GiftShop.DataAccess.Entities;
using GiftShop.DataAccess.UnitOfWork;
using GiftShop.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GiftShop.DataAccess.Context
{
    public static class DbInitializer
    {
        public static void Initialize(GiftShopContext context, UserService userService)
        {
            InitializeRoles(context);
            InitializeUsers(context, userService);
        }

        private static void InitializeRoles(GiftShopContext context)
        {
            if (context.Roles.Any())
                return;

            var roles = new List<Role>
            {
                new Role{  Name = "User" },
                new Role{  Name = "Manager" },
                new Role{  Name = "Admin" },
            };

            context.Roles.AddRange(roles);
            context.SaveChanges();
        }

        private static void InitializeUsers(GiftShopContext context, UserService userService)
        {
            if (context.Users.Any())
                return;

            var users = new List<User>
            {
                new User{ Id = Guid.NewGuid(), FirstName = "Andrei", LastName = "Mindru", Email = "amindru99@gmail.com", Password = "giftShop@123" },
            };

            users.ForEach(u => userService.Register(u));
            
            var usersRoles = new List<UsersRole>
            {
                new UsersRole{UserId = users[0].Id, RoleId = 3}
            };

            if (context.UsersRoles.Any())
                return;

            context.UsersRoles.AddRange(usersRoles);
            context.SaveChanges();
        }
    }
}
