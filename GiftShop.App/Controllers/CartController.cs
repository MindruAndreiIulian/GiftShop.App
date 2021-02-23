using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using GiftShop.App.Models.Order;
using GiftShop.App.Models.OrderItems;
using GiftShop.App.Models.Product;
using GiftShop.DataAccess.Entities;
using GiftShop.Domain.DTO;
using GiftShop.Services.CartServices;
using GiftShop.Services.OrderServices;
using GiftShop.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GiftShop.App.Controllers
{
    public class CartController : BaseController
    {
        private readonly ProductService _productService;
        private readonly OrderService _orderService;
        private readonly IMapper _mapper;
        private readonly CartService _cartService;
        private readonly ILogger<CartController> _logger;
        private readonly CurrentUser _currentUser;

        public CartController(ProductService productService ,CartService cartService,OrderService orderService, ILogger<CartController> logger, CurrentUser currentUser, IMapper mapper)
        {
            _productService = productService;
            _orderService = orderService;
            _mapper = mapper;
            _cartService = cartService;
            _logger = logger;
            _currentUser = currentUser;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (_currentUser.IsAuthenticated)
            {

                var cart = _cartService.GetCart(_currentUser.Id);
                
                List<ProductVm> products = _mapper.Map<List<Product>, List<ProductVm>>(_cartService.GetCartItems(cart.Id).ToList());
                List<OrderItemsVm> orderItems = _mapper.Map<List<OrderItem>, List<OrderItemsVm>>(cart.OrderItems.ToList());
                float total = 0;
                for (int i = 0; i < products.Count(); i++)
                    total += (float)products[i].Price * orderItems[i].Quantity;

                CartVm model = new CartVm { Id = cart.Id, Products = products, OrderItems = orderItems, Total = total };

                    return View("../Cart/Cart", model);
            }
            else
            {
                return RedirectToAction("Login","Account");
            }

        }




        public IActionResult AddToCart(int productId)
        {
            

            if (_currentUser.IsAuthenticated)
            {
                if (_cartService.AddProductToCart(productId, _currentUser.Id))
                    return Ok(new { message = "Added" });
                else
                    return BadRequest(new { message = "Product no longer available" });
            }
            else
            {
                return BadRequest(new { message = "Not authenticated"});
            }
        }

        public IActionResult RemoveFromCart(int productId)
        {
            _cartService.RemoveProduct(productId, _currentUser.Id,1);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveAll(int productId)
        {
            if (!_cartService.RemoveProduct(productId, _currentUser.Id, 2))
                return ForbiddenView();

            return RedirectToAction("Index", "Cart");

          
            
        }

        public IActionResult AddOneToCart(int productId)
        {
            _cartService.IncreaseCartQty(productId, _currentUser.Id);
            return RedirectToAction("Index");
        }

    
    }
}
