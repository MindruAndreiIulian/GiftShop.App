using GiftShop.Common.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable

namespace GiftShop.DataAccess.Entities
{
    public partial class Role : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
