using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GiftShop.App.Models.MediaModel
{
    public class MediaVm
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }
        public IFormFile File { get; set; } //folosita exclusiv pentru HTML 
    }
}
