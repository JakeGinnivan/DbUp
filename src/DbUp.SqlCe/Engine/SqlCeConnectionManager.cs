using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using DbUp.Engine.Transactions;
using DbUp.Support.SqlServer;
using DbUp.SqlCe.Engine;

namespace DbUp.SqlCe
{
    /// <summary>
    /// Manages SqlCe Database Connections
    /// </summary>
    public class SqlCeConnectionManager : DatabaseConnectionManager
    {
        /// <summary>
        /// Manages SqlCe Database Connections
        /// </summary>
        /// <param name="connectionString"></param>
        public SqlCeConnectionManager(string connectionString) : base(l => new SqlCeConnection(connectionString))
        {
            this._sqlContainer = new SqlCeStatements();
        }

        public override IEnumerable<string> SplitScriptIntoCommands(string scriptContents)
        {
            var commandSplitter = new SqlCommandSplitter();
            var scriptStatements = commandSplitter.SplitScriptIntoCommands(scriptContents);
            return scriptStatements;
        }
    }
}
