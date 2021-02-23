using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiftShop.DataAccess.Entities;
using GiftShop.DataAccess.UnitOfWork;
using GiftShop.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace GiftShop.Services.CartServices
{
   public class CartService: BaseService
    {
        public CartService(GiftShopUnitOfWork _unitOfWork):base(_unitOfWork)
        {

        }

        public IEnumerable<Product> GetCartItems(int orderId)
        {
            var order = _unitOfWork.Order.Query.Include(o => o.OrderItems).Where(o => o.Id == orderId && o.IsFinished == false).FirstOrDefault();
            var orderItems = new List<Product>();
            
            foreach(var oi in order.OrderItems)
            {
                Product p = _unitOfWork.Product.Query.FirstOrDefault(p => p.Id == oi.ProductId);
                orderItems.Add(p);
            }

            return orderItems;
        }

        public Order GetCart(int userId)
        {
            var cart = _unitOfWork.Order.Query.Include(o => o.OrderItems).Where(o => o.UserId == userId && o.IsFinished == false).FirstOrDefault();
            if (cart == null) {
                var newCart = new Order { CreatedDate = DateTime.Now, IsFinished = false, UserId = userId };
                _unitOfWork.Order.Add(newCart);
                if (_unitOfWork.SaveChanges())
                    return newCart;
            }

            return cart; 
        }

        public bool AddProductToCart(int itemId, int userId)
        {
            
            var cart = _unitOfWork.Order.Query.Include(o => o.OrderItems).Where(o => o.UserId == userId && o.IsFinished == false).FirstOrDefault();
            if(cart == null)
            {
                cart = new Order { CreatedDate = DateTime.Now, IsFinished = false, UserId = userId };
                _unitOfWork.Order.Add(cart);
            }

            var item = _unitOfWork.Product.Query.FirstOrDefault(p => p.Id == itemId);
            if (item.IsDeleted == true || item.StockNumber <= 0)
                return false;

            OrderItem orderItm = _unitOfWork.OrderItem.Query.FirstOrDefault(o => o.OrderId == cart.Id && o.ProductId == itemId);
           
                if(orderItm == null)
            {
                _unitOfWork.OrderItem.Add(new OrderItem { Order = cart, Product = item, OrderId = cart.Id, ProductId = item.Id, Quantity = 1 });
                item.StockNumber--;
                _unitOfWork.Product.Update(item);
            }
            else
            {
                orderItm = _unitOfWork.OrderItem.Query.FirstOrDefault(oi => oi.ProductId == item.Id && oi.OrderId == cart.Id);
                orderItm.Quantity++;
                _unitOfWork.OrderItem.Update(orderItm);
                item.StockNumber--;
                _unitOfWork.Product.Update(item);
            }

            return _unitOfWork.SaveChanges();
        }

        public bool RemoveProduct(int productId, int userId, int qty)
        {
            var cart = _unitOfWork.Order.Query.Include(o => o.OrderItems).Where(o => o.UserId == userId && o.IsFinished == false).FirstOrDefault();
            var item = _unitOfWork.Product.Query.FirstOrDefault(p => p.Id == productId);
            var orderItm = _unitOfWork.OrderItem.Query.FirstOrDefault(oi => oi.ProductId == item.Id && oi.OrderId == cart.Id);
            if (orderItm == null)
                return false;

            if (orderItm.Quantity == 1 || qty > 1)
            {
                _unitOfWork.OrderItem.Remove(orderItm);
                item.StockNumber += (byte)orderItm.Quantity;
                _unitOfWork.Product.Update(item);
            }
            else
            {
                orderItm.Quantity--;
                item.StockNumber++;
                _unitOfWork.Product.Update(item);
            }

            return _unitOfWork.SaveChanges();
        }

        public bool IncreaseCartQty(int productId, int userId)
        {
            var cart = _unitOfWork.Order.Query.Include(o => o.OrderItems).Where(o => o.UserId == userId && o.IsFinished == false).FirstOrDefault();
            var item = _unitOfWork.Product.Query.FirstOrDefault(p => p.Id == productId);
            var orderItm = _unitOfWork.OrderItem.Query.FirstOrDefault(oi => oi.ProductId == item.Id && oi.OrderId == cart.Id);
            if (item.StockNumber <= 0 || item.IsDeleted == true)
                return false;

            if(item != null)
            {
                orderItm.Quantity++;
                item.StockNumber--;
                _unitOfWork.Product.Update(item);
            }

            return _unitOfWork.SaveChanges();

        }
    }
}
