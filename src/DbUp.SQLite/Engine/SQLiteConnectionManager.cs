﻿using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text.RegularExpressions;
using DbUp.Engine.Transactions;
using DbUp.SQLite.Helpers;
using DbUp.SQLite.Engine;

namespace DbUp.SQLite
{
    /// <summary>
    /// SQLite Connection Manager.
    /// </summary>
    public class SQLiteConnectionManager : DatabaseConnectionManager
    {
        /// <summary>
        /// Creates new SQLite Connection Manager
        /// </summary>
        public SQLiteConnectionManager(string connectionString) : base(l => new SQLiteConnection(connectionString))
        {
            this.SqlContainer = new SQLiteStatements();
            this.SqlContainer = new SQLiteStatements();
        }

        /// <summary>
        /// Creates new SQLite Connection Manager
        /// </summary>
        public SQLiteConnectionManager(SharedConnection sharedConnection) : base(l => sharedConnection)
        {
        }

        /// <summary>
        /// Sqlite statements seprator is ; (see http://www.sqlite.org/lang.html)
        /// </summary>
        public override IEnumerable<string> SplitScriptIntoCommands(string scriptContents)
        {
            var scriptStatements =
                Regex.Split(scriptContents, "^\\s*;\\s*$", RegexOptions.IgnoreCase | RegexOptions.Multiline)
                    .Select(x => x.Trim())
                    .Where(x => x.Length > 0)
                    .ToArray();

            return scriptStatements;
        }
    }
}