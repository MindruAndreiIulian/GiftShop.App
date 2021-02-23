using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiftShop.DataAccess.Entities;
using GiftShop.DataAccess.UnitOfWork;
using GiftShop.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace GiftShop.Services.Products
{
    public class ProductService : BaseService
    {
        public ProductService(GiftShopUnitOfWork _unitOfWork) : base(_unitOfWork)
        {
            
        }

        public IEnumerable<Product> GetProductByName(string name)
        {
            return _unitOfWork.Product.Get().Where(p => p.Name.Contains(name) && p.IsDeleted != true).ToList();
        }

        public IEnumerable<Product> GetProducts()
        {
            return _unitOfWork.Product.Get().Where(p => p.IsDeleted != true).ToList();
        }

        public bool AddProduct(Product product)
        {
            _unitOfWork.Product.Add(product);
            return _unitOfWork.SaveChanges();
        }

        public bool UpdateProduct(Product product)
        {
            _unitOfWork.Product.Update(product);
            return _unitOfWork.SaveChanges();
        }

        public bool DeleteProduct(Product product)
        {
            _unitOfWork.Product.Remove(product);
            return _unitOfWork.SaveChanges();
        }

        public Product GetProductById(int id)
        {
            return _unitOfWork.Product.Query.FirstOrDefault(p => p.Id == id && p.IsDeleted != true);
        }
    }
}
