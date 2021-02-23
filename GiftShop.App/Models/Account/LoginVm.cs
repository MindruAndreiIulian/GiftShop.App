using System.ComponentModel.DataAnnotations;


namespace GiftShop.App.Models.Account
{
    public class LoginVm
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public bool AreCredentialsInvalid { get; set; }
        public string ErrorMessage { get; set; }
    }
}
