using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiftShop.App.Models.Base;
using GiftShop.App.Models.MediaModel;

namespace GiftShop.App.Models.Product
{
    public class ProductVm : BaseModel
    {
        public ProductVm()
        {
            MediaVm = new MediaVm();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public byte? StockNumber { get; set; }
        public bool? isDeleted { get; set; }
        public int? MediaId { get; set; }

        public MediaVm MediaVm { get; set; }

    }
}
