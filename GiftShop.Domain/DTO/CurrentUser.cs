using System;
using System.Collections.Generic;
using System.Text;

namespace GiftShop.Domain.DTO
{
    public class CurrentUser
    {
        public CurrentUser(bool isAuthenticated, bool isAdmin)
        {
            IsAuthenticated = isAuthenticated;
            IsAdmin = isAdmin;
        }

        public bool IsAuthenticated { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }



    }
}
