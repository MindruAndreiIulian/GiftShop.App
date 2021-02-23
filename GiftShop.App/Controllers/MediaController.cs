using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiftShop.Services.MediaServices;
using Microsoft.AspNetCore.Mvc;

namespace GiftShop.App.Controllers
{
    public class MediaController : BaseController
    {
        private readonly MediaService _mediaService;
        public MediaController(MediaService mediaService)
        {
            _mediaService = mediaService;
        }
        public IActionResult GetImage(int id)
        {
            var image = _mediaService.GetMedia(id);
            if (image == null)
            {
                return null;
            }
            var bytes = image.Content;

            return File(bytes, "image/jpg");
        }

    }
}
