using GiftShop.Common.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable

namespace GiftShop.DataAccess.Entities
{
    public partial class User : IEntity
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public DateTime? Birthdate { get; set; }
        public int? GenderId { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
