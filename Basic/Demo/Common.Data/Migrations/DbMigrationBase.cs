//------------------------------------------------------------------------------------------------------
// <copyright company="Robert M Jordan LLC" division="Bushido">
//     Copyright (c) Robert M. Jordan all rights reserved.
// </copyright>
//------------------------------------------------------------------------------------------------------

namespace Bushido.Common.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// An abstract base class for Entity Framework Migration classes
    /// </summary>
    public abstract class DbMigrationBase : DbMigration
    {
        #region <Fields & Contstants>

        protected readonly DbContext context; 

        public const string FOLDER_MIGRATIONS = @"Migrations";

        #endregion

        #region <Constructors>

        public DbMigrationBase()
        {
        }

        public DbMigrationBase(DbContext context)
        {
            this.context = context;
        }

        #endregion

        #region <Properties>

        public string MigrationFolderPath
        {
            get
            {
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                if (HttpContext.Current != null)
                    baseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");

                return Path.Combine(baseDirectory, FOLDER_MIGRATIONS);
            }
        }

        #endregion

        #region <Methods>

        #region private

        protected void ExecuteSqlScript(string sqlScript)
        {
            sqlScript = sqlScript.Replace("\r\n\tgo", "\r\nGO");
            sqlScript = sqlScript.Replace("\r\n\tGo", "\r\nGO");
            sqlScript = sqlScript.Replace("\r\n\tgO", "\r\nGO");
            sqlScript = sqlScript.Replace("\r\n\tGO", "\r\nGO");

            sqlScript = sqlScript.Replace("go\t\r\n", "GO\r\n");
            sqlScript = sqlScript.Replace("Go\t\r\n", "GO\r\n");
            sqlScript = sqlScript.Replace("gO\t\r\n", "GO\r\n");
            sqlScript = sqlScript.Replace("GO\t\r\n", "GO\r\n");

            sqlScript = sqlScript.Replace("\r\ngo\r\n", "\r\nGO\r\n");
            sqlScript = sqlScript.Replace("\r\nGo\r\n", "\r\nGO\r\n");
            sqlScript = sqlScript.Replace("\r\ngO\r\n", "\r\nGO\r\n");

            string[] sql = sqlScript.Split(new[] { "\r\nGO\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var sqlCommand in sql)
            {
                var sqlToRun = sqlCommand;
                if (!string.IsNullOrWhiteSpace(sqlToRun))
                {
                    if (sqlToRun.ToLowerInvariant().EndsWith("go"))
                        sqlToRun = sqlToRun.Substring(0, sqlCommand.Length - 2);

                    if (sqlToRun.ToLowerInvariant().StartsWith("go"))
                        sqlToRun = sqlToRun.Substring(2);

                    if (context != null)
                    {
                        context.Database.ExecuteSqlCommand(sqlToRun);
                    }
                    else
                    {
                        Sql(sqlToRun);
                    }
                }
            }
        }

        #endregion

        #region public

        public void ExecuteSqlScripts(string folderPath)
        {
            if (folderPath.StartsWith(@"\"))
                folderPath = folderPath.Substring(1);

            var directoryPath = Path.Combine(MigrationFolderPath, folderPath);

            if (!Directory.Exists(directoryPath))
                return;

            var files = Directory.EnumerateFiles(directoryPath, "*.*", SearchOption.AllDirectories)
                                 .Where(f => f.ToLowerInvariant()
                                     .EndsWith(".sql"))
                                 .OrderBy(f => f);

            foreach (var file in files)
            {
                var query = File.ReadAllText(file);

                if (string.IsNullOrEmpty(query))
                    continue;

                ExecuteSqlScript(query);
            }
        }

        #endregion

        #endregion
    }
}
