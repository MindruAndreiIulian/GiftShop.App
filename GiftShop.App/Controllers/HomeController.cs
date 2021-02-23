using GiftShop.App.Models;
using GiftShop.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GiftShop.App.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CurrentUser CurrentUser;
        public HomeController(ILogger<HomeController> logger, CurrentUser currentUser)
        {
            _logger = logger;
            CurrentUser = currentUser;
        }

        public IActionResult Index()
        {
            return View();
            //return RedirectToAction("NotFoundView","BaseController");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("{*url}", Order = 999)]
        public IActionResult Page404()
        {
            Response.StatusCode = 404;
            return View();
        }

    }
}
