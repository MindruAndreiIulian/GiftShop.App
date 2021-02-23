using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiftShop.App.Models.Base;
using GiftShop.App.Models.Order;

namespace GiftShop.App.Models.User
{
    public class UserVm:BaseModel
    {
        public UserVm()
        {
            Orders = new HashSet<OrderVm>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? Birthdate { get; set; }
        public int? GenderId { get; set; }
        public int? roleId { get; set; }

        public virtual ICollection<OrderVm> Orders { get; set; }
    }
}

