//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.Data.Migrations.DemoDb
{
    using System.Data.Entity;
    using System.IO;

    // <note for="Package Manage Console Commands">
    //     <see cref="http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/migrations-and-deployment-with-the-entity-framework-in-an-asp-net-mvc-application"/>
    // 
    //         - Enable-Migrations -ContextTypeName DemoDbContext
    //         - Add-Migration DemoDbInitial -ConfigurationTypeName DemoDbConfiguration
    //         - Update-Database -SourceMigration DemoDbInitialMigration -ConfigurationTypeName DemoDbConfiguration
    // 
    //     <command name="Enable-Migrations">Creates a Migrations folder and craetes a Configuration.cs file. (run once at the beginning only)</commands>
    //     <command name="Update-Database">This will simply update the database schema...not the data.</commands>
    //     <command name="Add-Migration">This will simply add a new migration (if completely new make sure you delete migrations from DB).</commands>
    // </note>

    /// <summary>
    /// A concrete migration for the DemoDb database
    /// </summary>
    public class DemoDbInitialMigration : DbMigrationBase
    {
        #region <Fields & Constants>

        private const string FOLDER_MIGRATION = @"DemoDb\Initial";

        #endregion

        #region <Constructors>

        public DemoDbInitialMigration()
        {
        }

        public DemoDbInitialMigration(DbContext context) : base(context)
        {

        }

        #endregion

        #region <Methods>

        #region private

        private void UpSchema_DBO()
        {
            // NOTE: Typically, I prefer to put tables in a DATA schema, but I will forgoe this for the demo

            CreateTable(
                "dbo.BankAccount",
                c => new
                {
                    BankAccountId = c.Int(nullable: false, identity: true),
                    BankAccountTypeId = c.Int(nullable: false),
                    OwnerFullName = c.String(nullable: false, maxLength: 50),
                    Balance = c.Decimal(nullable: false, storeType: "money"),
                    AnnualPercentageRate = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.BankAccountId)
                .ForeignKey("dbo.BankAccountType", t => t.BankAccountTypeId, cascadeDelete: true)
                .Index(t => t.BankAccountTypeId);

            CreateTable(
                "dbo.BankAccountType",
                c => new
                {
                    BankAccountTypeId = c.Int(nullable: false, identity: true),
                    BankAccountTypeName = c.String(nullable: false, maxLength: 50),
                })
                .PrimaryKey(t => t.BankAccountTypeId);
        }

        private void DownSchema_DBO()
        {
            DropForeignKey("dbo.BankAccount", "BankAccountTypeId", "dbo.BankAccountType");
            DropIndex("dbo.BankAccount", new[] { "BankAccountTypeId" });
            DropTable("dbo.BankAccountType");
            DropTable("dbo.BankAccount");
        }

        #endregion

        #region public

        public void Seed(DemoDbContext context)
        {
        }

        public override void Up()
        {
            ExecuteSqlScripts(Path.Combine(FOLDER_MIGRATION, "Pre-Deployment Scripts"));

            UpSchema_DBO();

            ExecuteSqlScripts(Path.Combine(FOLDER_MIGRATION, "Views"));
            ExecuteSqlScripts(Path.Combine(FOLDER_MIGRATION, "Functions"));
            ExecuteSqlScripts(Path.Combine(FOLDER_MIGRATION, "Stored Procedures"));
            ExecuteSqlScripts(Path.Combine(FOLDER_MIGRATION, "Triggers"));
            ExecuteSqlScripts(Path.Combine(FOLDER_MIGRATION, "Assemblies"));
            ExecuteSqlScripts(Path.Combine(FOLDER_MIGRATION, "Post-Deployment Scripts"));
        }

        public override void Down()
        {
            DownSchema_DBO();
        }

        #endregion

        #endregion
    }
}
