using GiftShop.Common.Interfaces;
using GiftShop.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiftShop.DataAccess.Base
{
    public class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {

        protected GiftShopContext Context { get; }

        public IQueryable<TEntity> Query { get; }

        public BaseRepository(GiftShopContext context)
        {
            Context = context;
            Query = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public IQueryable<TEntity> Get()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
        }
    }



}
