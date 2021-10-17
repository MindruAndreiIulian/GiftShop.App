using GiftShop.Common.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable

namespace GiftShop.DataAccess.Entities
{
    public partial class FavoriteProduct : IEntity
    {
        public Guid UserId { get; set; }
        public int ProductId { get; set; }
    }
}
