using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiftShop.DataAccess.Entities;
using GiftShop.DataAccess.UnitOfWork;
using GiftShop.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace GiftShop.Services.OrderServices
{
    public class OrderService: BaseService
    {
        public OrderService(GiftShopUnitOfWork _unitOfWork): base(_unitOfWork)
        {
            
        }

        public Order GetOrderById(int id, bool getItems)
        {
            if (!getItems) return _unitOfWork.Order.Query.FirstOrDefault(o => o.Id == id);
            else return _unitOfWork.Order.Query.Include(o => o.OrderItems).FirstOrDefault(o => o.Id == id);

        }

        public Dictionary<Product,int> GetOrderItems(Order order)
        {
            Dictionary<Product, int> orderItems = new Dictionary<Product, int>();
            foreach(var o in order.OrderItems)
            {
                var product = _unitOfWork.Product.Query.FirstOrDefault(p => p.Id == o.ProductId);
                var qty = o.Quantity;
                orderItems.Add(product, qty);
            }

            return orderItems;

        }

        public IEnumerable<Order> GetOrders(bool includeItems)
        {
            if (includeItems)
            {
                return _unitOfWork.Order.Query.Include(o => o.OrderItems)
                                              .ThenInclude(oi => oi.Product)
                                              .ToList();
            }

            return _unitOfWork.Order.Get().ToList();
        } 

        public double CalculateTotal(Order order)
        {
            if (order.OrderItems == null)
                return 0;

            double sum = 0;
            foreach(var oi in order.OrderItems)
            {
                var product = _unitOfWork.Product.Query.FirstOrDefault(p => p.Id == oi.ProductId);
                sum += (double)product.Price * oi.Quantity;
            }

            return sum;
        }
       
        public IEnumerable<Order> GetOrdersByUserId(int id)
        {
            return _unitOfWork.Order.Query.Include(o => o.OrderItems).Where(o => o.UserId == id).ToList();
        }

       public bool FinishOrder(Order order)
        {
            _unitOfWork.Order.Update(order);
            return _unitOfWork.SaveChanges();
        }


    }
}
