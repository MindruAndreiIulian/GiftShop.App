using AutoMapper;
using GiftShop.App.Models.Account;
using GiftShop.DataAccess.Entities;
using GiftShop.Domain.DTO;
using GiftShop.Services.Users;
using HospitalScheduler.WebApp.Code;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GiftShop.App.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserService _userService;
        private readonly CurrentUser CurrentUser;
        private readonly IMapper _mapper;
        private MailSender _mailSender;

        public AccountController(UserService userService, CurrentUser currentUser, IMapper mapper)
        {
            _mailSender = new MailSender();
            _userService = userService;
            CurrentUser = currentUser;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Register(RegisterVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = _mapper.Map<User>(model);

            try
            {
                if (_userService.Register(user)) {
                    _mailSender.SendWelcomeMail(model.Email);

                    return RedirectToAction("Index", "Home");
                } else
                {
                    model.IsInvalid = true;
                    model.ErrorMessage = "Email already registered";
                    return View(model);
                }

            }
            catch (System.Net.Mail.SmtpException)
            {
                return ForbiddenView();
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterVm());
        }

        [HttpGet]
        public IActionResult Login()
        {


            return View(new LoginVm());
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = _userService.Login(model.Email, model.Password);
            if (user == null)
            {
                    model.AreCredentialsInvalid = true;
                    model.ErrorMessage = "Username or password invalid";
                    return View(model);
            }
            await LogIn(user);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await LogOut();
            return RedirectToAction("Index", "Home");
        }


        private async Task LogIn(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
                
            };

            var identity = new ClaimsIdentity(claims, "GiftShopCookies");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                    scheme: "GiftShopCookies",
                    principal: principal);
        }
        private async Task LogOut()
        {
            await HttpContext.SignOutAsync(scheme: "GiftShopCookies");
        }
    }
}
