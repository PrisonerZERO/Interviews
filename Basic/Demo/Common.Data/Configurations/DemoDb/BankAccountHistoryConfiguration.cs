//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.Data
{
    using Models.DemoDb;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration;

    /// <summary>
    /// A concrete entity configuration class for the DemoDb database
    /// </summary>
    public class BankAccountHistoryConfiguration : EntityTypeConfiguration<BankAccountHistory>
    {
        #region <Constructors>

        public BankAccountHistoryConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankAccountHistory>()
                .Property(e => e.Balance)
                .HasPrecision(19, 4);
        }

        #endregion
    }
}
