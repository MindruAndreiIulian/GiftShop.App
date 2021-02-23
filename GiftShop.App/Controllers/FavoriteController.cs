using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GiftShop.App.Models.Product;
using GiftShop.DataAccess.Entities;
using GiftShop.Domain.DTO;
using GiftShop.Services.FavoriteServices;
using GiftShop.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace GiftShop.App.Controllers
{
    public class FavoriteController : BaseController
    {
        readonly IMapper _mapper;
        readonly CurrentUser _currentUser;
        readonly ProductService _productService;
        readonly FavoriteService _favoriteService;

        public FavoriteController(ProductService productService, FavoriteService favoriteService, CurrentUser currentUser, IMapper mapper)
        {
            _mapper = mapper;
            _currentUser = currentUser;
            _productService = productService;
            _favoriteService = favoriteService;
        }

        public IActionResult Index()
        {
            if (!_currentUser.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            var products = _favoriteService.GetFavoriteProducts(_currentUser.Id).ToList();

            return View("../Favorites/Index", _mapper.Map<List<Product>,List<ProductVm>>(products));
        }

        public IActionResult AddToFav(int productId)
        {
            if (!_currentUser.IsAuthenticated)
                return BadRequest(new ClientErrorData { Title = "Unauthenticated" });

            var prod = _productService.GetProductById(productId);
            if(prod == null || !_favoriteService.AddToFavorite(new FavoriteProduct { UserId = _currentUser.Id, ProductId = prod.Id }))
            {
                return BadRequest(new ClientErrorData { Title = "Error"});
            }

            return RedirectToAction("Index", "Favorite");
        }

        public IActionResult RemoveFromFav(int productId)
        {
            if(!_currentUser.IsAuthenticated)
                return BadRequest(new ClientErrorData { Title = "Unauthenticated" });

            var prod = _productService.GetProductById(productId);
            if (prod == null || !_favoriteService.RemoveFromFavorite(new FavoriteProduct { UserId = _currentUser.Id, ProductId = prod.Id }))
            {
                return BadRequest(new ClientErrorData { Title = "Error" });
            }

            return RedirectToAction("Index", "Favorite");
        }
    }
}
