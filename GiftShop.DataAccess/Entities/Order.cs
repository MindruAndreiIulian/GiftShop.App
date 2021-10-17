using GiftShop.Common.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable

namespace GiftShop.DataAccess.Entities
{
    public partial class Order : IEntity
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public Guid UserId { get; set; }
        public bool IsFinished { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
