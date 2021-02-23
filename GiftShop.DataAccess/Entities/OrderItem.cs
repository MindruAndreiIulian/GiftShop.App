﻿using GiftShop.Common.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable

namespace GiftShop.DataAccess.Entities
{
    public partial class OrderItem : IEntity
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? OrderId { get; set; }
        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}