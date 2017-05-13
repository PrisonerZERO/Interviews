//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    /// <summary>
    /// A DbContext interface
    /// </summary>
    public interface IDbContext : IDisposable
    {
        #region <Properties>

        Database Db { get; }

        #endregion

        #region <Methods>

        DbEntityEntry Entry(object entity);

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        DbSet Set(Type entityType);

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();

        #endregion
    }
}
