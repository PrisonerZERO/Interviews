//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.Data
{
    using Models.DemoDb;
    using System.Data.Entity;

    /// <summary>
    /// A DbContext interface for the DemoDb database
    /// </summary>
    public interface IDemoDbContext : IDbContext
    {
        #region <Properties>

        string ConnectionString { get; }

        #region dbsets

        #region dbo

        DbSet<BankAccount> BankAccounts { get; set; }
        DbSet<BankAccountType> BankAccountTypes { get; set; }

        #endregion

        #endregion

        #endregion
    }
}
