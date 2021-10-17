using GiftShop.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GiftShop.DataAccess.Entities
{
    public class Category : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
