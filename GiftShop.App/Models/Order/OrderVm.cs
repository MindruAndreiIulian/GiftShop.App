using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiftShop.App.Models.OrderItems;
using GiftShop.App.Models.Product;
using GiftShop.DataAccess.Entities;

namespace GiftShop.App.Models.Order
{
    public class OrderVm
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public string userName { get; set; }
        public bool IsFinished { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<OrderItemsVm> OrderItems { get; set; }
        public double? Total { get; set; }
        public Dictionary<ProductVm, int> ProductQty { get; set; }
        public OrderVm() {
            OrderItems = new List<OrderItemsVm>();
            ProductQty = new Dictionary<ProductVm, int>();
        }


       
    }
}
