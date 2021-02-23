using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftShop.App.Models.Product
{
    public class ProductListVm
    {
        public List<ProductVm> ProductList { get; set; }

        public ProductListVm()
        {
            ProductList = new List<ProductVm>();
        }
    }
}
