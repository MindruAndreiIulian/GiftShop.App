using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftShop.App.Models.OrderItems
{
    public class OrderItemsVm
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? OrderId { get; set; }
        public int Quantity { get; set; }

   
    }
}
