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
    public class BankAccountTypeConfiguration : EntityTypeConfiguration<BankAccountType>
    {
        #region <Constructors>

        public BankAccountTypeConfiguration(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BankAccountType>()
                .HasMany(e => e.BankAccounts)
                .WithRequired(e => e.BankAccountType)
                .WillCascadeOnDelete(false);
        }

        #endregion
    }
}
