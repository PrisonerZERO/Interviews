//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Transactions;

    /// <summary>
    /// A generic base class for concrete repositories
    /// </summary>
    /// <typeparam name="TEntity">Any Entity Framework model</typeparam>
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region <Fields>

        protected readonly DbSet<TEntity> dbSet;
        protected IDbContext dbContext;

        #endregion

        #region <Constructors>

        public GenericRepository(IDbContext dbcontext)
        {
            dbContext = dbcontext;
            dbSet = dbContext.Set<TEntity>();
        }

        #endregion


        #region <Methods>

        public virtual IQueryable<TEntity> GetActive()
        {
            return dbSet;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return dbSet;
        }

        public virtual TEntity GetById(object id)
        {
            var context = ((IObjectContextAdapter)dbContext).ObjectContext;
            var entity = null as TEntity;

            using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                entity = dbSet.Find(id);
            }

            if (entity != null)
                context.Refresh(RefreshMode.StoreWins, entity);

            return entity;
        }

        public virtual TEntity Find(params object[] keyValues)
        {
            var entity = null as TEntity;

            using (new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
            {
                entity = dbSet.Find(keyValues);
            }

            return entity;
        }

        public virtual TEntity Add(TEntity entity)
        {
            var currentState = GetState(entity);

            if (currentState == EntityState.Detached)
                ApplyState(entity, EntityState.Added);

            return dbSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            var currentState = GetState(entity);

            if (currentState == EntityState.Detached)
            {
                dbSet.Attach(entity);
                ApplyState(entity, EntityState.Modified);
            }
        }

        public virtual void Delete(object id)
        {
            var entity = dbSet.Find(id);
            
            ApplyState(entity, EntityState.Deleted);
        }

        public virtual void Delete(TEntity entity)
        {
            var currentState = GetState(entity);

            if (currentState == EntityState.Detached)
                ApplyState(entity, EntityState.Deleted);
        }

        public virtual void ApplyState(TEntity entity, EntityState state)
        {
            dbContext.Entry(entity).State = state;
        }

        public virtual EntityState GetState(TEntity entity)
        {
            return dbContext.Entry(entity).State;
        }

        public virtual IQueryable<TEntity> GetAllIncludeChildren(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] children)
        {
            foreach (var item in children)
            {
                dbSet.Include(item).Load();
            }

            return dbSet.Where(filter);
        }

        #endregion
    }
}
