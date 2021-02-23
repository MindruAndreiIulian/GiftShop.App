using GiftShop.DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace GiftShop.Services.Base
{
    public class BaseService
    {
        protected readonly GiftShopUnitOfWork _unitOfWork;

        public BaseService(GiftShopUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
