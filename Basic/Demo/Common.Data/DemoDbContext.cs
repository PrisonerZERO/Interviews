//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.Data
{
    using Common.Framework.Configuration;
    using Common.Models.DemoDb;
    using Microsoft.Practices.Unity;
    using System.Configuration;
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// A DbContext for the DemoDb database
    /// </summary>
    public class DemoDbContext : DbContextBase, IDemoDbContext
    {
        #region <Fields & Constants>

        private string connectionString = null;

        #endregion

        #region <Constructors>

        public DemoDbContext(string connectionString)
        {
            Database.Connection.ConnectionString = connectionString;
            Init();
        }

        [InjectionConstructor]
        public DemoDbContext()
        {
            Database.Connection.ConnectionString = ConnectionString;
            Init();
        }

        #endregion

        #region <Properties>

        public string ConnectionString { get { return connectionString ?? (connectionString = GetConnectionString()); } }

        #region dbsets

        #region dbo

        public virtual DbSet<BankAccount> BankAccounts { get; set; }
        public virtual DbSet<BankAccountHistory> BankAccountHistory { get; set; }
        public virtual DbSet<BankAccountType> BankAccountTypes { get; set; }

        #endregion

        #endregion

        #endregion

        #region <Methods>

        #region private

        private void Init()
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer<DemoDbContext>(null);
        }

        private string GetConnectionString()
        {
            var key = Settings.ConnectionString.Database.DemoDb;

            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

        #endregion

        #region protected

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // MODELS - CONFIGURATION
            modelBuilder.Configurations.Add(new BankAccountConfiguration(modelBuilder));
            modelBuilder.Configurations.Add(new BankAccountHistoryConfiguration(modelBuilder));
            modelBuilder.Configurations.Add(new BankAccountTypeConfiguration(modelBuilder));
        }

        #endregion

        #region public

        public static DemoDbContext Create()
        {
            return new DemoDbContext();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        #endregion

        #endregion
    }
}
