//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.Framework.Configuration
{
    /// <summary>
    /// A partial class to hold settings
    /// </summary>
    public partial class Settings
    {
        #region <Classes>

        public partial class ConnectionString 
        {
            #region <Classes>

            public partial class Database
            {
                #region <Properties>

                public static string DemoDb = "ConnectionString.Database.DemoDb";

                #endregion
            }

            #endregion
        }

        #endregion
    }
}
