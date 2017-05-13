//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.Data
{
    using Models.DemoDb;

    /// <summary>
    /// An interface for Unit of Work
    /// </summary>
    public interface IDemoDbUnitOfWork : IUnitOfWork
    {
        #region <Properties>

        #region repositories

        IRepository<BankAccount> BankAccounts { get; }
        IRepository<BankAccountType> BankAccountTypes { get; }

        #endregion

        #endregion
    }
}
