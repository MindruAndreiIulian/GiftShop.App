using GiftShop.Common.Interfaces;
using GiftShop.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GiftShop.DataAccess.Base
{
    public class BaseUnitOfWork : IUnitOfWork, IDisposable
    {
        protected readonly GiftShopContext context;


        public BaseUnitOfWork(GiftShopContext context)
        {
            this.context = context;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public bool SaveChanges()
        {
            try
            {
                return context.SaveChanges() != 0;
            }
            catch(Exception ex)
            {
                //to do 
                //write in a log file
                return false;
            }
        }
        
        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await context.SaveChangesAsync() != 0;
            }
            catch(Exception ex)
            {
                //to do 
                //write in a log file
                return false;
            }
        }
    }
}
