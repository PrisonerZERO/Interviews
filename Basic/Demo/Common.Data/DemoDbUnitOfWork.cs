//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.Data
{
    using Models.DemoDb;
    using Microsoft.Practices.Unity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    /// <summary>
    /// A concrete Unit of Work
    /// </summary>
    public class DemoDbUnitOfWork : IDemoDbUnitOfWork, IDisposable
    {
        #region <Constructors>

        [InjectionConstructor]
        public DemoDbUnitOfWork()
        {
        }

        #endregion

        #region <Destructors>

        ~DemoDbUnitOfWork()
        {
            // Finalizer
            Dispose(false);
        }

        #endregion

        #region <Properties>

        [Dependency()]
        public IDbContext DbContext { get; set; }

        #region Repositories

        #region Dbo Repositories

        [Dependency]
        public IRepository<BankAccount> BankAccounts { get; set; }

        [Dependency]
        public IRepository<BankAccountType> BankAccountTypes { get; set; }

        #endregion

        #endregion

        #endregion

        #region <Methods>

        #region protected

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (DbContext != null)
                    DbContext.Dispose();
            }
        }

        #endregion

        #region public

        public void RefreshAll()
        {
            var objectContext = ((IObjectContextAdapter)DbContext).ObjectContext;
            var manager = objectContext.ObjectStateManager;
            var refreshableObjects = (from entry in manager.GetObjectStateEntries(
                                                EntityState.Added
                                               | EntityState.Deleted
                                               | EntityState.Modified
                                               | EntityState.Unchanged)
                                      where entry.EntityKey != null
                                      select entry.Entity);

            objectContext.Refresh(RefreshMode.StoreWins, refreshableObjects);
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #endregion
    }
}
