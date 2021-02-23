using GiftShop.App.Models.User;

namespace GiftShop.App.Models.Order
{
    public class FinishOrderVm
    {
        public CartVm Cart { get; set; }
        public UserVm User { get; set; }
       
    }
}
