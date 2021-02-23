using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GiftShop.App.Models.Order;
using GiftShop.App.Models.OrderItems;
using GiftShop.App.Models.Product;
using GiftShop.App.Models.User;
using GiftShop.DataAccess.Entities;
using GiftShop.Domain.DTO;
using GiftShop.Services.CartServices;
using GiftShop.Services.OrderServices;
using GiftShop.Services.Users;
using HospitalScheduler.WebApp.Code;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GiftShop.App.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly CartService _cartService;
        private readonly IMapper _mapper;
        private readonly OrderService _orderService;
        private readonly ILogger<OrdersController> _logger;
        private readonly CurrentUser _currentUser;
        private readonly UserService _userService;
        private MailSender _mailSender;

        public OrdersController(UserService userService, OrderService orderServices, ILogger<OrdersController> logger,
                                CurrentUser currentUser, IMapper mapper, CartService cartService)
        {
            _cartService = cartService;
            _mapper = mapper;
            _orderService = orderServices;
            _logger = logger;
            _currentUser = currentUser;
            _userService = userService;
            _mailSender = new MailSender();
        }



        [HttpGet]
        public IActionResult UserOrders()
        {
           
                if (!_currentUser.IsAuthenticated)
                    return RedirectToAction("Login", "Account");

                var orders = _orderService.GetOrdersByUserId(_currentUser.Id).Where(o => o.IsFinished != false);
                
                if (orders == null)
                {
                    return NotFound();
                }

                return View("../Order/UserOrders", _mapper.Map<IEnumerable<Order>, IEnumerable<OrderVm>>(orders));
            
            

        }

        [HttpGet]
        public IActionResult FinishOrder(int orderId)
        {
            var cart = _cartService.GetCart(_currentUser.Id);
            if (cart == null || cart.OrderItems.Count() == 0)
                return ForbiddenView();

            List<ProductVm> products = _mapper.Map<List<Product>, List<ProductVm>>(_cartService.GetCartItems(cart.Id).ToList());
            List<OrderItemsVm> orderItems = _mapper.Map<List<OrderItem>, List<OrderItemsVm>>(cart.OrderItems.ToList());
            float total = 0;
            for (int i = 0; i < products.Count(); i++)
                total += (float)products[i].Price * orderItems[i].Quantity;
            CartVm model = new CartVm { Id = cart.Id, Products = products, OrderItems = orderItems, Total = total };


            FinishOrderVm finish = new FinishOrderVm { Cart = model, User = _mapper.Map<User, UserVm>(_userService.GetUserById(_currentUser.Id)) };

            if (finish == null)
                return View();

            return View("../Order/FinishOrder", finish);
        }

        [HttpPost]
        public IActionResult FinishOrder(FinishOrderVm model)
        {
            if (!ModelState.IsValid || !_currentUser.IsAuthenticated)
            {
                return ForbiddenView();
            }

            if(model.Cart.OrderItems.Count() == null)
            {
                return ForbiddenView();
            }

            if (!model.User.Equals(_userService.GetUserById(_currentUser.Id))) {
                var existingUser = _userService.GetUserById(_currentUser.Id);
                existingUser.LastName = model.User.LastName;
                existingUser.FirstName = model.User.FirstName;
                existingUser.Address = model.User.Address;

                _userService.UpdateUserData(existingUser);
            }

            var order = _orderService.GetOrderById(model.Cart.Id, false);
            order.IsFinished = true;
            order.CreatedDate = DateTime.Now;

            try
            {
                _orderService.FinishOrder(order);
                _mailSender.SendOrderConfirmation(model.User.Email, order);
                _mailSender.SendOrderConfirmation(model.User.Email, order);

                return RedirectToAction("Index", "Home");
            }
            catch (System.Net.Mail.SmtpException)
            {
                return ForbiddenView();
            }
        }


        [HttpGet]
        public IActionResult ViewOrder(int orderId)
        {
            if (!_currentUser.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            var order = _orderService.GetOrderById(orderId,true);
            if (order == null)
                return ForbiddenView();
            var orderItems = _mapper.Map<List<OrderItemsVm>>(order.OrderItems);
            var productQty = _orderService.GetOrderItems(order);
            var model = _mapper.Map<Dictionary<Product, int>, Dictionary<ProductVm, int>>(productQty);

                return View("../Order/ViewOrder", new OrderVm { CreatedDate = order.CreatedDate,
                                                                Id = order.Id, IsFinished = true,
                                                                OrderItems = orderItems, 
                                                                Total = _orderService.CalculateTotal(order),
                                                                UserId = order.UserId,   
                                                                ProductQty = model});
        }
    } 
}
