using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiftShop.App.Models.Account
{
    public class RegisterVm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public DateTime? Birthdate { get; set; }
        public string GenderId { get; set; }
        public bool IsInvalid { get; set; }
        public string ErrorMessage { get; set; }
    }
}
