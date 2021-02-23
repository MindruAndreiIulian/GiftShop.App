using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiftShop.App.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GiftShop.DataAccess.Entities;
using GiftShop.Common.Interfaces;
using GiftShop.Services.Products;
using GiftShop.App.Models.Product;
using GiftShop.Domain.DTO;
using System.Buffers;
using AutoMapper;

namespace GiftShop.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly ProductService _productService;
        private readonly ILogger<ProductsController> _logger;
        private readonly CurrentUser _currentUser;
        private readonly IMapper __mapper;

        public ProductsController(ProductService productService, ILogger<ProductsController> logger, CurrentUser currentUser, IMapper mapper)
        {
            _productService = productService;
            _logger = logger;
            _currentUser = currentUser;
            __mapper = mapper;
        }

        public IActionResult ProductPage(int productId)
        {
            var product = _productService.GetProductById(productId);
            if(product != null)
            {

                return View(__mapper.Map<Product, ProductVm>(product));
            }
            else
            {
                return BadRequest();
            }
        }
    
        public IActionResult GetProductByName(string name)
        { 
            var model = __mapper.Map<List<Product>,List<ProductVm>>(_productService.GetProductByName(name).ToList());
       
            return Json(
                new { model }
                );
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = _productService.GetProducts().ToList();
            if(products == null)
            {
                return NotFound();
            }

            var model = new ProductListVm();
            foreach(var product in products)
            {
                var productVm = new ProductVm()
                {

                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    MediaId = product.MediaId
                };
                model.ProductList.Add(productVm);
            }

            return View(model);
  
        }

       



    }
}
