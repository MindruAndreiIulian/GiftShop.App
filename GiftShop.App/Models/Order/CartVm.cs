using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiftShop.App.Models.OrderItems;
using GiftShop.App.Models.Product;

namespace GiftShop.App.Models.Order
{
    public class CartVm
    {
        public int Id { get; set; }
        public IEnumerable<ProductVm> Products { get; set; }
        public IEnumerable<OrderItemsVm> OrderItems { get; set; }
        public float Total { get; internal set; }

        public CartVm()
        {
            Products = new List<ProductVm>();
            OrderItems = new List<OrderItemsVm>();
        }        
    }
}
