//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.Data.Migrations.DemoDb
{
    using System.Data.Entity.Migrations;

    /// <summary>
    /// A DbConfiguration class for the DemoDb database
    /// </summary>
    public class DemoDbConfiguration : DbMigrationsConfiguration<DemoDbContext>
    {
        #region <Constructors>

        public DemoDbConfiguration()
        {
            // NOTE: Setting to 'false' forces use of the Add-Migration command to scaffold migrations
            AutomaticMigrationsEnabled = false;
        }
        
        #endregion

        #region <Methods>

        #region protected

        protected override void Seed(DemoDbContext context)
        {
            base.Seed(context);

            // NOTE: UP is automatically executed for you...do not call it again
            var currentDbMigration = new DemoDbInitialMigration(context);
            currentDbMigration.Seed(context);
        }

        #endregion

        #endregion
    }
}
