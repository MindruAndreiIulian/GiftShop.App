using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiftShop.DataAccess.Entities;
using GiftShop.DataAccess.UnitOfWork;
using GiftShop.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace GiftShop.Services.FavoriteServices
{
    public  class FavoriteService:BaseService
    {
        public FavoriteService(GiftShopUnitOfWork _unitOfWork):base(_unitOfWork)
        {

        }

        public bool AddToFavorite(FavoriteProduct product)
        {
            if (product == null)
                return false;

            _unitOfWork.FavProduct.Add(product);
            return _unitOfWork.SaveChanges();
        }

        public bool RemoveFromFavorite(FavoriteProduct product)
        {
            if (product == null)
                return false;

            _unitOfWork.FavProduct.Remove(product);
            return _unitOfWork.SaveChanges();
        }

        public IEnumerable<Product> GetFavoriteProducts(Guid userId)
        {
            var favProducts = _unitOfWork.FavProduct.Query.Where(fp => fp.UserId == userId).ToList();
            var products = new List<Product>();
            foreach(var fp in favProducts)
            {
                var product = _unitOfWork.Product.Query.FirstOrDefault(p => p.Id == fp.ProductId);
                products.Add(product);
            }

            return products;
        }

    }
}
