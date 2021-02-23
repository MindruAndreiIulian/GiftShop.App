using System;
using System.Collections.Generic;
using System.Text;
using GiftShop.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace GiftShop.DataAccess.Entities
{
    public class Media: IEntity
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }

        public virtual Product Product { get; set; }
    }
}
