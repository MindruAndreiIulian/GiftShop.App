using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GiftShop.App.Models.Order;
using GiftShop.App.Models.Product;
using GiftShop.App.Models.User;
using GiftShop.DataAccess.Entities;
using GiftShop.Domain.DTO;
using GiftShop.Services.OrderServices;
using GiftShop.Services.Products;
using GiftShop.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace GiftShop.App.Controllers
{
    public class AdministratorController : BaseController
    {

        private readonly IMapper _mapper;
        private readonly CurrentUser _currentUser;
        private readonly ProductService _productService;
        private readonly OrderService _orderService;
        private readonly UserService _userService;

        public AdministratorController(IMapper mapper, CurrentUser currentUser, ProductService productService, OrderService orderService, UserService userService)
        {
            _mapper = mapper;
            _currentUser = currentUser;
            _productService = productService;
            _orderService = orderService;
            _userService = userService;
        }

        public IActionResult ChangeUserRole(Guid userId, int roleId)
        {
            if (!_currentUser.IsAdmin || !_currentUser.IsAuthenticated)
                return ForbiddenView();

            var user = _userService.GetUserById(userId);
            if(user != null)
            {
                if (roleId == 1)
                {
                    _userService.RemoveRole(user);
                    _userService.ChangeRole(user, 1);
                    
                    return View();
                }
                else if (roleId == 2)
                {
                    _userService.RemoveRole(user);
                    _userService.ChangeRole(user, 2);
        
                    return View();
                }
                else if (roleId == 3)
                {
                    _userService.RemoveRole(user);
                    _userService.ChangeRole(user, 3);
                   
                    return View();
                }
                else return ForbiddenView();
            }
            else
            {
                return ForbiddenView();
            }
        }


        public IActionResult RemoveUserRole(Guid userId, int roleId)
        {
            if (!_currentUser.IsAdmin || !_currentUser.IsAuthenticated)
                return ForbiddenView();

            var user = _userService.GetUserById(userId);
            if (user != null)
            {
                if (roleId == 1)
                {
                    _userService.ChangeRole(user, 1);
                    return View();
                }
                else if (roleId == 2)
                {
                    var didUpdate = _userService.ChangeRole(user, 2);
                    return View();
                }
                else if (roleId == 3)
                {
                    _userService.ChangeRole(user, 3);
                    return View();
                }
                else return ForbiddenView();
            }
            else
            {
                return ForbiddenView();
            }
        }

        public IActionResult DeleteProduct(int productId)
        {
            if (!_currentUser.IsAdmin || !_currentUser.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            var product = _productService.GetProductById(productId);
            if (product.IsDeleted != false)
            {
                product.IsDeleted = true;
                _productService.UpdateProduct(product);
            }

            return RedirectToAction("ViewProducts", "Administrator");
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            if (!_currentUser.IsAdmin || !_currentUser.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            return View(new ProductVm());
        }

        [HttpGet]
        public IActionResult ViewUsers()
        {
            if (!_currentUser.IsAdmin || !_currentUser.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            if (_currentUser.IsAdmin)
            {
                var usersRoles = _userService.getAllUsers();
                var model = new List<UserVm>();
                foreach (var ur in usersRoles)
                    if (ur != null)
                    {
                        var user = _mapper.Map<User, UserVm>(ur.User);
                        user.roleId = ur.RoleId;
                        model.Add(user);
                    }

                return View(model);
            }
            else
            {
                return ForbiddenView();
            }
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            if (!_currentUser.IsAdmin || !_currentUser.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            if (_currentUser.IsAdmin)
            {
                IEnumerable<Order> orders = _orderService.GetOrders(true).ToList();


                if (orders == null)
                {
                    return NotFound();
                }


                return View("../Administrator/Orders", _mapper.Map<IEnumerable<Order>, IEnumerable<OrderVm>>(orders));
            }
            else
            {
                return ForbiddenView();
            }

        }

        

        [HttpGet]
        public IActionResult ViewProducts()
        {
            if (!_currentUser.IsAdmin || !_currentUser.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            var products = _productService.GetProducts().ToList();
            if (products == null)
            {
                return NotFound();
            }

            var model = new ProductListVm();
            foreach (var product in products)
            {
                var productVm = new ProductVm()
                {

                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    MediaId = product.MediaId,
                    StockNumber = product.StockNumber
                };
                model.ProductList.Add(productVm);
            }

            return View(model);

        }

        [HttpGet]
        public IActionResult UpdateProduct(int productId)
        {
            if (!_currentUser.IsAdmin || !_currentUser.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            var prod = _productService.GetProductById(productId);
            return View(_mapper.Map<Product,ProductVm>(prod));
        }

        [HttpPost]
        public IActionResult UpdateProduct(ProductVm model)
        {
            if (!_currentUser.IsAdmin || !_currentUser.IsAuthenticated)
                return RedirectToAction("Login", "Account");


            if (_currentUser.IsAdmin)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (_productService.GetProductById(model.Id) == null)
                {
                    return View(model);
                }

                var productToBeUpdated = _mapper.Map<Product>(model);

                var existingProduct = _productService.GetProductById(model.Id);

                if (model.MediaVm != null)
                {
                    model.MediaVm.Content = FileToByteArray(model.MediaVm.File);
                    var media = _mapper.Map<Media>(model.MediaVm);
                    productToBeUpdated.Media = media;
                }

                if (existingProduct!= null)
                {
                    existingProduct.Description = productToBeUpdated.Description;
                    existingProduct.Media = productToBeUpdated.Media;
                    existingProduct.MediaId = productToBeUpdated.MediaId;
                    existingProduct.Name = productToBeUpdated.Name;
                    existingProduct.OrderItems = productToBeUpdated.OrderItems;
                    existingProduct.Price = productToBeUpdated.Price;
                    existingProduct.StockNumber = productToBeUpdated.StockNumber;
                    if (_productService.UpdateProduct(existingProduct))
                    {
                        model.DisplayMessage = "Successfuly saved";
                        model.IsOkay = true;
                    }
                    else
                    {
                        model.DisplayMessage = "Error";
                        model.IsOkay = false;
                    }

                }
                return View(model);
            }
            else
            {
                return ForbiddenView();
            }

        }

        
        public IActionResult AddProduct(ProductVm model)
        {
            if (!_currentUser.IsAdmin || !_currentUser.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            if (_currentUser.IsAdmin)
            {

                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var product = _mapper.Map<Product>(model);

                if (model.MediaVm != null)
                {
                    model.MediaVm.Content = FileToByteArray(model.MediaVm.File);
                    var media = _mapper.Map<Media>(model.MediaVm);
                    product.Media = media;
                }
                if (_productService.AddProduct(product))
                {
                    model.DisplayMessage = "Successfuly saved";
                    model.IsOkay = true;
                }
                else
                {
                    model.DisplayMessage = "Error";
                    model.IsOkay = false;
                }
                return View(model);
            }
            else
            {
                return ForbiddenView();
            }
        }


        [HttpGet]
        public IActionResult Index()
        {
            if (_currentUser.IsAdmin)
                return View();
            else
                return ForbiddenView();
        }

       [HttpGet]
       public IActionResult GetProductsForSearch(string name)
        {
            var results = _productService.GetProductByName(name).Select(p => new { id = p.Id, text = p.Name, image =p.MediaId }).ToList();
            var json = new { results };
            return Ok(json);
        }

        [HttpGet]
        public IActionResult GetSales()
        {
            if (!_currentUser.IsAdmin || !_currentUser.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            var query = _orderService.GetOrders(true).Where(o => o.IsFinished == true);
            var orders = query.Select(o => new { CreatedDate = o.CreatedDate.ToString("MM/dd/yyyy"),
                                                 Value = _orderService.CalculateTotal(o)
                                      });

            var result = orders .GroupBy(o => o.CreatedDate)
                                .OrderBy(o => o.First().CreatedDate)
                                .Select(t => new { date = t.First().CreatedDate, sale = t.Sum(tg => tg.Value) })
                                .ToList();

            var json = new { result };
            return Ok(json);

          
        }

    }
}
