using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GiftShop.App.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult NotFoundView()
        {
            return View();
        }

        public ActionResult ForbiddenView()
        {
            return View("../Home/Page404");
        }

        protected static byte[] FileToByteArray(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    return ms.ToArray();
                }
            }
            return null;
        }

    }
}
