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
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// An interface for Entity Repository's
    /// </summary>
    /// <typeparam name="TEntity">Any Entity Framework model</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        #region <Methods>

        IQueryable<TEntity> GetActive();

        IQueryable<TEntity> GetAll();

        TEntity GetById(object id);

        TEntity Find(params object[] keyValues);

        TEntity Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(object id);

        void ApplyState(TEntity entity, EntityState state);

        EntityState GetState(TEntity entity);

        IQueryable<TEntity> GetAllIncludeChildren(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] children);
        
        #endregion
    }
}
