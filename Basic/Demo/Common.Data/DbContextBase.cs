//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.Data
{
    using System;
    using System.Data.Entity;

    /// <summary>
    /// A DbContext base
    /// </summary>
    public class DbContextBase : DbContext, IDbContext, IDisposable
    {
        #region <Fields>

        private readonly Guid instanceId;
        private bool disposed;

        #endregion

        #region <Constructors>

        public DbContextBase()
        {
            instanceId = Guid.NewGuid();
        }

        public DbContextBase(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            instanceId = Guid.NewGuid();
        }

        #endregion

        #region <Properties>

        public Database Db { get { return Database; } }

        public Guid InstanceId { get { return instanceId; } }

        #endregion

        #region <Methods>

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
                base.Dispose(disposing);

            disposed = true;
        }

        #endregion
    }
}
