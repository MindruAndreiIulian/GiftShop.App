using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GiftShop.DataAccess.Entities;
using GiftShop.DataAccess.UnitOfWork;
using GiftShop.Services.Base;

namespace GiftShop.Services.MediaServices
{
    public class MediaService : BaseService
    {
        public MediaService(GiftShopUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Media GetMedia(int id)
        {
            return _unitOfWork.Media.Query.FirstOrDefault(m => m.Id == id);
        }


    }
}
