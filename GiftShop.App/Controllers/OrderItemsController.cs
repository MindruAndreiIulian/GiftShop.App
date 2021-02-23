using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GiftShop.App.Models.OrderItems;
using GiftShop.DataAccess.Entities;
using GiftShop.Domain.DTO;
using GiftShop.Services.OrderServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GiftShop.App.Controllers
{
    public class OrderItemsController:BaseController
    {
        private readonly IMapper _mapper;
        private readonly OrderService _orderService;
        private readonly ILogger<OrderItemsController> _logger;
        

        public OrderItemsController(OrderService orderServices, ILogger<OrderItemsController> logger, IMapper mapper)
        {
            _mapper = mapper;
            _orderService = orderServices;
            _logger = logger;
          
        }

        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var order = _orderService.GetOrderById(orderId, true);
            if (order != null) 
                return View(".. / Order / Orders", _mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemsVm>>(order.OrderItems));

            return NotFound();
        }
    }
}
