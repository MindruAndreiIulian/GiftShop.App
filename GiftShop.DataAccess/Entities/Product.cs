using GiftShop.Common.Interfaces;
using System.Collections.Generic;

#nullable disable

namespace GiftShop.DataAccess.Entities
{
    public partial class Product : IEntity
    {
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public byte? StockNumber { get; set; }

        public int? MediaId { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual Media Media { get; set; }
    }
}
