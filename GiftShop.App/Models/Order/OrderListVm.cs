using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftShop.App.Models.Order
{
    public class OrderListVm
    {
       public List<OrderVm> OrderList { get; set; }

        public OrderListVm()
        {
            OrderList = new List<OrderVm>();
        }
    }
}
