using GiftShop.Domain.DTO;
using GiftShop.Services.OrderServices;
using GiftShop.Services.Products;
using GiftShop.Services.CartServices;
using GiftShop.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GiftShop.Services.MediaServices;
using GiftShop.Services.FavoriteServices;

namespace GiftShop.App.Code
{
    public static class ExtensionMethods
    {
        public static IServiceCollection AddCurrentUser(this IServiceCollection services)
        {
            services.AddScoped(serviceProvider =>
            {
                var contextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
                var context = contextAccessor.HttpContext;
                var email = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty;
                var userService = serviceProvider.GetService<UserService>();
                var user = userService.GetUsersByEmail(email);
           
                if (user != null)
                {
                    var isAdmin = userService.CheckAdmin(email);
                    if (isAdmin)
                    {
                        return new CurrentUser(isAuthenticated: true, isAdmin: true)
                        {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,

                            
                        };
                    } else
                    {
                        return new CurrentUser(isAuthenticated: true, isAdmin: false)
                        {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,

                        };
                    }
                }


                return new CurrentUser(isAuthenticated: false, isAdmin:false);
            });

            return services;
        }


        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<ProductService>();
            services.AddScoped<OrderService>();
            services.AddScoped<CartService>();
            services.AddScoped<MediaService>();
            services.AddScoped<FavoriteService>();

            return services;
        }

    }
}
